package com.example.unihack;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
//import com.example.unihack.reso.R;
//import com.example.unihack.reso.models.RegisterModel;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import org.json.JSONException;
import org.json.JSONObject;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

public class RegisterActivity extends AppCompatActivity {

    private EditText mEmailEditText;
    private EditText mPasswordEditText;
    private EditText mConfPassEditText;
    private Button mRegisterButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        mEmailEditText = (EditText) findViewById(R.id.et_activity_register_email);
        mPasswordEditText = (EditText) findViewById(R.id.et_activity_register_password);
        mRegisterButton = (Button) findViewById(R.id.b_activity_register_register);
        mConfPassEditText = (EditText) findViewById(R.id.et_activity_register_confpass);

        mRegisterButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    Register();
                    //tre sa mai fac intentu aici sa mearga pe pagina de login
                }
                catch (Exception e){
                    return;
                }
            }
        });
    }

    private void Register() throws JSONException {

        String email = mEmailEditText.getText().toString();
        String password = mPasswordEditText.getText().toString();
        String confirmpassword = mConfPassEditText.getText().toString();

        RegisterModel registerModel = new RegisterModel();
        registerModel.Email=email.trim();
        registerModel.Password=password;
        registerModel.ConfirmPassword=confirmpassword;

        String url = "https://unihackapi.azurewebsites.net/api/Account/Register";

        Map<String,String> obj = new HashMap<String,String>();
        obj.put("Email", registerModel.Email);
        obj.put("Password", registerModel.Password);
        obj.put("ConfirmPassword", registerModel.ConfirmPassword);

        JSONObject parameters = new JSONObject(obj);
        Log.d("Tag",parameters.toString());
        JsonObjectRequest jsRequest = new JsonObjectRequest(url, parameters, new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Toast.makeText(RegisterActivity.this,"Succes!", Toast.LENGTH_SHORT).show();
                RegisterActivity.this.finish();
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
                Toast.makeText(RegisterActivity.this,error.toString(), Toast.LENGTH_SHORT).show();
            }
        }){
            public Map<String,String> getHeaders() throws AuthFailureError {
                Map<String, String> params = new HashMap<String, String>();
                params.put("Content-Type", "application/json; charset=utf-8");
                return params;
            }

            protected Response<JSONObject> parseNetworkResponse(NetworkResponse response) {
                try {
                    String jsonString = new String(response.data,
                            HttpHeaderParser.parseCharset(response.headers, PROTOCOL_CHARSET));

                    JSONObject result = null;

                    if (jsonString != null && jsonString.length() > 0)
                        result = new JSONObject(jsonString);

                    return Response.success(result,
                            HttpHeaderParser.parseCacheHeaders(response));
                } catch (UnsupportedEncodingException e) {
                    return Response.error(new ParseError(e));
                } catch (JSONException je) {
                    return Response.error(new ParseError(je));
                }
            }
        };
        RequestQueue queue = Volley.newRequestQueue(this);
        jsRequest.setRetryPolicy(new DefaultRetryPolicy(5000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));

        queue.add(jsRequest);
    }
}
