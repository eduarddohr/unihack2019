package com.example.bluetooth;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.Method;
import java.util.UUID;
import com.example.bluetooth.R;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.Build;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class MainActivity extends AppCompatActivity {
    private static final String TAG = "bluetooth";
    Button btnOn, btnOff;
    TextView txtArduino;
    Handler h;
    final int RECIEVE_MESSAGE = 1;  // status pt.
    //private int flag=0;
    private BluetoothAdapter btAdapter = null;
    private BluetoothSocket btSocket = null;

    private String test = "12 323~";
    int endOfLineDist = test.indexOf("~");
    String test1 = test.substring(0, endOfLineDist);
    String[] separated = test1.split(" ");
    int distance = Integer.parseInt(separated[0]);
    int weight = Integer.parseInt(separated[1].trim());

    float capacity = (float)((((20 - distance) * 0.5)*100 + (2000 - weight) * 0.5)/20);


    private StringBuilder sb = new StringBuilder();
    private ConnectedThread mConnectedThread;
    private static final UUID MY_UUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); //asta asa trebuie pus UUID pt SPP
    private static String address = "00:18:91:D6:C9:96"; //adresa MAC pt. bluetooth

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.layout); //aleg layout
        btnOn = (Button)findViewById(R.id.btnOn);   // buton led ON
        btnOff = (Button) findViewById(R.id.btnOff);  // buton led OFF
        txtArduino = (TextView) findViewById(R.id.txtArduino);  // casuta unde primim datele
        h = new Handler() {
            public void handleMessage(android.os.Message msg) {
                switch (msg.what) {
                    case RECIEVE_MESSAGE:                                           //daca am primit mesaj
                        byte[] readBuf = (byte[]) msg.obj;
                        String strIncom = new String(readBuf, 0, msg.arg1);   //citesc buffer(1 byte)
                        // create string from bytes array
                        sb.append(strIncom);
                        int endOfLineDist = sb.indexOf("~");    //DACA SCHIMBI IN ARDUINO CU Serial.println() POTI VERIFICA AICI CU \n\r( pt. ca asta e enter).
                        //int endOfLineWeight = sb.indexOf("*");

                        if (endOfLineDist > 0 ) {                                    // daca am ajuns la capat de mesaj
                            String sbprint = sb.substring(0, endOfLineDist);        // extrag string
                            sb.delete(0, sb.length());                               //sterg ce a fost in el
                                                     // scriu in casuta

                            String[] separated = sbprint.split(" ");
                            int distance = Integer.parseInt(separated[0]);
                            int weight = Integer.parseInt(separated[1].trim());

                            float capacityDistance = (distance * 5);
                            float weightDistnace = (float)(weight * 0.05);

                            

                            txtArduino.setText(sbprint);
                            btnOff.setEnabled(true);                                 //las butoanele sa se poata apasa
                            btnOn.setEnabled(true);
                        }
                        /*
                        if(endOfLineWeight > 0 ){                                       //daca am ajuns la capat de mesaj greutate
                            String sbprint = sb.substring(0, endOfLineWeight);        // extrag string
                            sb.delete(0, sb.length());                           //sterg ce a fost in el
                            txtArduino.setText("Greutatea este: " + sbprint+ " ");         // scriu in casuta
                            btnOff.setEnabled(true);                                 //las butoanele sa se poata apasa
                            btnOn.setEnabled(true);
                        }
                        */

                        break;
                }
            };
        };
        btAdapter = BluetoothAdapter.getDefaultAdapter();       // adaptor bluetooth
        checkBTState();
        btnOn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                //btnOn.setEnabled(false);
                mConnectedThread.write("1");    // trimit "1" prin bluetooth
            }
        });
        btnOff.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                //btnOff.setEnabled(false);
                mConnectedThread.write("0");    // trimit "2" prin bluetooth
                txtArduino.setText("");
            }
        });
    }
    private BluetoothSocket createBluetoothSocket(BluetoothDevice device) throws IOException {      //creez socket
        if(Build.VERSION.SDK_INT >= 10){
            try {
                final Method  m = device.getClass().getMethod("createInsecureRfcommSocketToServiceRecord", new Class[] { UUID.class });
                return (BluetoothSocket) m.invoke(device, MY_UUID);
            } catch (Exception e) {
                Log.e(TAG, "Could not create Insecure RFComm Connection",e);
            }
        }
        return  device.createRfcommSocketToServiceRecord(MY_UUID);
    }
    @Override
    public void onResume() {
        super.onResume();
        Log.d(TAG, "...onResume...");
        BluetoothDevice device = btAdapter.getRemoteDevice(address);    // setam device cu adresa catre care creem socket
        try {
            btSocket = createBluetoothSocket(device);
        } catch (IOException e) {
            errorExit("Fatal Error", "In onResume() and socket create failed: " + e.getMessage() + ".");
        }
        btAdapter.cancelDiscovery();            //oprim descoperirea
        Log.d(TAG, "...Conectare...");
        try {
            btSocket.connect();                 //facem conexiunea
            Log.d(TAG, "....Conexiune ok...");
        } catch (IOException e) {
            try {
                btSocket.close();               //inchidem daca a aparut ceva eroare
            } catch (IOException e2) {
                errorExit("Fatal Error", "In onResume() and unable to close socket during connection failure" + e2.getMessage() + ".");
            }
        }
        Log.d(TAG, "...Creem Socket...");
        mConnectedThread = new ConnectedThread(btSocket);   //creeam un stream ca sa poata arduino sa trimita date
        mConnectedThread.start();                           //pornim stream-ul
    }
    @Override
    public void onPause() {         //ce se intampla cand e pe pauza
        super.onPause();
        Log.d(TAG, "...onPause...");
        try     {
            btSocket.close();
        } catch (IOException e2) {
            errorExit("Fatal Error", "In onPause() and failed to close socket." + e2.getMessage() + ".");
        }
    }
    private void checkBTState() {     //verificam daca exista bluetooth si daca e pornit
        if(btAdapter==null) {
            errorExit("Fatal Error", "Bluetooth not support");
        } else {
            if (btAdapter.isEnabled()) {
                Log.d(TAG, "...Bluetooth ON...");
            } else {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);     //spunem user-ului sa il aprinda daca e stins
                startActivityForResult(enableBtIntent, 1);
            }
        }
    }

    private void errorExit(String title, String message){
        Toast.makeText(getBaseContext(), title + " - " + message, Toast.LENGTH_LONG).show();
        finish();
    }
    private class ConnectedThread extends Thread {      //definire thread
        private final InputStream mmInStream;
        private final OutputStream mmOutStream;

        public ConnectedThread(BluetoothSocket socket) {
            InputStream tmpIn = null;
            OutputStream tmpOut = null;
            try {
                tmpIn = socket.getInputStream();
                tmpOut = socket.getOutputStream();
            } catch (IOException e) {
                Log.d(TAG, "...Eroare la stream: " + e.getMessage() + "...");

            }
            mmInStream = tmpIn;              //capat de citire
            mmOutStream = tmpOut;            //capat de scriere
        }

        public void run() {
            byte[] buffer = new byte[256];  // buffer pt. stream
            int bytes;
            while (true) {                  //ascultam stream-ul pana se intampla ceva
                try {
                    bytes = mmInStream.read(buffer);                //se citesc "bytes" bytes
                    h.obtainMessage(RECIEVE_MESSAGE, bytes, -1, buffer).sendToTarget();     // trimitem mesaj la handler
                } catch (IOException e) {
                    break;
                }
            }
        }

        public void write(String message) {
            byte[] msgBuffer = message.getBytes();
            try {
                mmOutStream.write(msgBuffer);       //scrierea in stream
            } catch (IOException e) {
                Log.d(TAG, "...Eroare la trimitere: " + e.getMessage() + "...");
            }
        }
    }
}