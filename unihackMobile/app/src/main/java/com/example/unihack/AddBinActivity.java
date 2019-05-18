package com.example.unihack;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

public class AddBinActivity extends AppCompatActivity {

    private EditText mNameEditText;
    private EditText mType;
    private EditText mLongitude;
    private EditText mLatitude;
    private Button mAddBinButton;

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_bin_activity);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        InitialiseViews();

        mAddBinButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                try {
                    addBin();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void InitialiseViews()
    {
        mNameEditText = findViewById(R.id.et_activity_add_bin_name);
        mType = findViewById(R.id.et_activity_add_bin_type);
        mAddBinButton = findViewById(R.id.btn_activity_add_bin_add);
        mLatitude=findViewById(R.id.et_activity_add_bin_latitude);
        mLongitude=findViewById(R.id.et_activity_add_bin_longitude);
    }

    private void addBin() throws JSONException{

        final UserHandler userHandler = new UserHandler(this);

        final AddBinModel mBinAddModel = new AddBinModel();
        mBinAddModel.Name = mNameEditText.getText().toString();
        mBinAddModel.Type = Integer.parseInt(mType.getText().toString());
        mBinAddModel.Latitude =  Float.parseFloat(mLatitude.getText().toString());
        mBinAddModel.Longitude = Float.parseFloat(mLongitude.getText().toString());
        mBinAddModel.Capacity = 0;

        String url = "https://unihackapi.azurewebsites.net/api/Bins/addBin";
        final UserHandler data = new UserHandler(this);
        String model = new Gson().toJson(mBinAddModel);
        JSONObject parameters = new JSONObject(model);
        Log.d("Tag",parameters.toString());
        JsonObjectRequest jsonRequest = new JsonObjectRequest(Request.Method.POST, url, parameters, new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Toast.makeText(AddBinActivity.this,"Succes!", Toast.LENGTH_SHORT).show();

                BinHelper.tempBin = mBinAddModel;
                AddBinActivity.this.finish();
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
                Toast.makeText(AddBinActivity.this,error.toString(), Toast.LENGTH_SHORT).show();
            }
        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> map = new HashMap<>();

                map.put("Content-Type", "application/json;charset=utf-8");
                map.put("Authorization", "Bearer " + data.getAccessToken());
                return map;
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
        jsonRequest.setRetryPolicy(new DefaultRetryPolicy(5000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        queue.add(jsonRequest);
    }

}
