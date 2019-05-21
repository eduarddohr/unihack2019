package com.example.bluetooth;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.bluetooth.R;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.os.Build;
import android.os.Handler;
import android.service.autofill.UserData;
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
    TextView txtArduino, txtArduino2, txtArduino3;
    Handler h;
    final int RECIEVE_MESSAGE = 1;  // status pt.
    //private int flag=0;
    private BluetoothAdapter btAdapter = null;
    private BluetoothSocket btSocket = null;



    private StringBuilder sb = new StringBuilder();
    private ConnectedThread mConnectedThread;
    private static final UUID MY_UUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); //asta asa trebuie pus UUID pt SPP
    private static String address = "00:18:91:D6:C9:96"; //adresa MAC pt. bluetooth

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.layout); //aleg layout
        txtArduino = (TextView) findViewById(R.id.txtArduino);
        txtArduino2 = (TextView) findViewById(R.id.txtArduino2);
        txtArduino3 = (TextView) findViewById(R.id.txtArduino3);

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
                            Log.d("mesaul:", sbprint);
                            if(sbprint.length() > 2) {
                                try {
                                    String[] separated = sbprint.split(" ");
                                    int distance = Integer.parseInt(separated[0]);
                                    int weight = Integer.parseInt(separated[1].trim());

                                    if(distance <= 20) {
                                        float cD = 90 - (distance * 5);
                                        float cW = (float) (weight * 0.05);
                                        float Capacity = max(cD, cW);  //asta trimite Edu la baza de date


                                        updateBin(Capacity);
                                        txtArduino.setText(String.valueOf(Capacity));
                                        txtArduino2.setText(String.valueOf(distance));
                                        txtArduino3.setText(String.valueOf(weight));
                                    }
                                }
                                catch (Exception e){
                                    Log.d("eroare la parse", e.toString());
                                }
                            }
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
    }

    private float max(float dist,float weight){
        if(dist > weight) return dist;
        else return weight;
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
    private void updateBin(float capacity){
        String urlString = "http://unihackapi.azurewebsites.net/api/Bins/UpdateBin?";
        urlString += "Id=A070AE78-34AB-446D-94D4-32CA127EBD7D";
        urlString += "&Capacity=";
        urlString += String.valueOf(capacity);

    /*
        StringRequest stringRequest = new StringRequest(urlString, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {

            }
        }, new Response.ErrorListener() { //Create an error listener to handle errors appropriately.
            @Override
            public void onErrorResponse(VolleyError error) {
                //This code is executed if there is an error.
            }
        });

        RequestQueue queue = Volley.newRequestQueue(this);
        stringRequest.setRetryPolicy(new DefaultRetryPolicy(5000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        queue.add(stringRequest);
        */

        StringRequest stringRequest = new StringRequest(urlString,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        try {


                        }catch (Exception e)
                        {
                            Log.d("eroare: ", e.toString());
                        }
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
                Toast.makeText(MainActivity.this,error.toString(), Toast.LENGTH_SHORT).show();

            }
        }) {

            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> map = new HashMap<>();
                map.put("Content-Type", "charset=utf-8");
                return map;
            }
            @Override

            protected Response<String> parseNetworkResponse(NetworkResponse response) {
                try {
                    String jsonString = new String(response.data,
                            HttpHeaderParser.parseCharset(response.headers));

                    return Response.success(jsonString,
                            HttpHeaderParser.parseCacheHeaders(response));
                } catch (UnsupportedEncodingException e) {
                    return Response.error(new ParseError(e));
                }
            }
        };

        RequestQueue queue = Volley.newRequestQueue(this);
        stringRequest.setRetryPolicy(new DefaultRetryPolicy(5000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        queue.add(stringRequest);

    }
}