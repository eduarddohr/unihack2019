package com.example.unihack;

import com.google.gson.annotations.SerializedName;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class SignInResponse {

    @SerializedName("access_token")
    private String accessToken;

    @SerializedName("userName")
    private String mail;

    /*
    @SerializedName("id")
    private UUID userId;
    */
    public UserData userDataObject;

    public String getAccessToken() {
        return accessToken;
    }

    public void setAccessToken(String accessToken) {
        this.accessToken = accessToken;
    }

    public JSONObject getUserDataJSON(){
        Map<String,String> map = new HashMap<>();
        map.put("userName",mail);
       // map.put("id",userId.toString());
        JSONObject obj= new JSONObject(map);
        return obj;
    }

   // public String getUserId(){ return userId.toString(); }

    public String getUserName(){ return mail; }
}
