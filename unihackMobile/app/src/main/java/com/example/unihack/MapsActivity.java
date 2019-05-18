package com.example.unihack;

import android.content.Intent;
import android.graphics.Color;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.DefaultRetryPolicy;
import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.PolylineOptions;
import com.google.gson.Gson;


import org.json.JSONArray;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

public class MapsActivity extends FragmentActivity implements OnMapReadyCallback {

    private GoogleMap mMap;
    private UserData userData;
    private HashMap<Marker, UUID> markersDictionary = new HashMap<>();
    private FloatingActionButton mAddIssueButton;
    public List<BinGetModel> Bins= new ArrayList<>();   // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    UserHandler handler = new UserHandler(this);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        userData = new UserHandler(this).getUserData();
        //button listener
        mAddIssueButton = findViewById(R.id.btn_activity_issues_fab);
        mAddIssueButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MapsActivity.this, AddBinActivity.class);
                startActivity(intent);
            }
        });

    }
    private void goToList(){
        Intent intent = new Intent(this, ListBinsActivity.class);
        startActivity(intent);
    }

    private void showAllBins(final String accessToken){

        String url = "https://unihackapi.azurewebsites.net/api/Bins/getBins";
        StringRequest stringRequest = new StringRequest(url,
                new Response.Listener<String>(){
                    @Override
                    public void onResponse(String response){
                        try{

                            JSONArray jsonArray = new JSONArray(response);
                            UserHandler userDataHandler = new UserHandler(MapsActivity.this);
                            for(int i=0; i<jsonArray.length(); i++){
                                JSONObject issueObj = jsonArray.getJSONObject(i);
                                String binString  = issueObj.toString();

                                ObjectMapper mapper = new ObjectMapper();
                                BinGetModel issue = mapper.readValue(binString, BinGetModel.class);
                                Bins.add(issue);
                            }
                            if(Bins.size()!=0){
                                UserData userData = handler.getUserData();
                                setupMap();
                            }
                        }catch(Exception e)
                        {
                            Log.d("Eroare ",e.toString());
                            e.toString();
                        }
                    }
                },new Response.ErrorListener(){
            @Override
            public void onErrorResponse(VolleyError error){
                error.printStackTrace();
                Toast.makeText(MapsActivity.this,error.toString(), Toast.LENGTH_SHORT).show();
            }
        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {       //punem header-u
                Map<String, String> map = new HashMap<>();
                map.put("Authorization", "Bearer " + accessToken);
                return map;
            }

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
        stringRequest.setRetryPolicy(new DefaultRetryPolicy(10000,
                DefaultRetryPolicy.DEFAULT_MAX_RETRIES,
                DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));

        queue.add(stringRequest);
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;

        UserHandler h = new UserHandler(this);
        String accessToken = null;
        if ((accessToken = h.getAccessToken()) == null)
        {
            return;
        }
        showAllBins(accessToken);
    }

    public void setupMap(){

        for (BinGetModel bin : Bins) {

            LatLng location = new LatLng(bin.Latitude, bin.Longitude);
            try {
                MarkerOptions marker = new MarkerOptions();

                if(bin.Capacity>=0 && bin.Capacity<=33) {
                            marker.position(location)
                            .title(bin.Name)
                            .icon(BitmapDescriptorFactory.fromResource(R.drawable.green));
                }
                else if(bin.Capacity>33 && bin.Capacity<=66)
                {
                            marker.position(location)
                            .title(bin.Name)
                            .icon(BitmapDescriptorFactory.fromResource(R.drawable.yellow));
                }
                else if(bin.Capacity>66 && bin.Capacity<=100)
                {
                            marker.position(location)
                            .title(bin.Name)
                            .icon(BitmapDescriptorFactory.fromResource(R.drawable.red));
                }

                Marker amarker = mMap.addMarker(marker);
                markersDictionary.put(amarker, bin.Id);
                mMap.addMarker(marker);
            }catch(Exception e){
                Log.d("Eroare ",e.toString());
            }
        }
        BinGetModel firstIssue = Bins.get(0);
        mMap.moveCamera(CameraUpdateFactory.newLatLng(new LatLng(firstIssue.Latitude, firstIssue.Longitude)));
        /*
        mMap.setOnMarkerClickListener(new GoogleMap.OnMarkerClickListener() {
            @Override
            public boolean onMarkerClick(Marker marker) {
                UUID binId = markersDictionary.get(marker);
                showIssueScreen(binId);
                return false;
            }
        });
        */
    }
    protected void onResume() {
        super.onResume();

        if (BinHelper.tempBin == null)
        {
            return;
        }

        AddBinModel bin = BinHelper.tempBin;

        Log.d("Issue nostru",bin.Name);

        LatLng location = new LatLng(bin.Latitude , bin.Longitude );

        MarkerOptions marker = new MarkerOptions()
                .position(location)
                .title(bin.Name);
                //.icon(BitmapDescriptorFactory.fromResource(R.drawable.icon));

        Marker amarker = mMap.addMarker(marker);
        markersDictionary.put( amarker, bin.Id);

        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(new LatLng(location.latitude, location.longitude),13f));
        mMap.addMarker(marker);

        BinGetModel newBin = new BinGetModel(bin);
        Bins.add(newBin);

        Log.d("Bins noastre sunt ",new Gson().toJson(Bins));

        UserHandler h = new UserHandler(this);
        String accessToken = null;
        if ((accessToken = h.getAccessToken()) == null)
        {
            return;
        }
        showAllBins(accessToken);

        BinHelper.tempBin = null;
    }
    /*
    private void showIssueScreen(UUID binId) {
        Intent intent = new Intent(MapsActivity.this, BinActivity.class);
        BinGetModel selectedBin = null;
        for(BinGetModel bin : Bins)
        {
            if (bin.Id == binId)
            {
                selectedBin = bin;
                break;
            }
        }
        if (selectedBin == null)
        {
            return;
        }
        intent.putExtra(Constants.BIN_ID_TAG, new Gson().toJson(selectedBin));
        startActivity(intent);
    }
    */


}
