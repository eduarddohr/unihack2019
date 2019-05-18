package com.example.unihack;

import android.content.Context;

import com.example.unihack.SignInResponse;
import com.example.unihack.UserData;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class UserHandler {
    private Context context;

    public UserHandler(Context context) {
        this.context = context;
    }

    public void setUserdata(SignInResponse response)
    {
        SharedPreferencesHandler handler = new SharedPreferencesHandler(context);
        handler.SetString(Constants.ACCESS_TOKEN_TAG, response.getAccessToken());
       // handler.SetString(Constants.USER_ID_TAG, response.getUserId());
        handler.SetString(Constants.USER_NAME_TAG, response.getUserName());
    }

    public void setUserdata(UserData response)
    {
        SharedPreferencesHandler handler = new SharedPreferencesHandler(context);
        handler.SetString(Constants.USER_NAME_TAG, response.FullName);
        //handler.SetString(Constants.USER_ID_TAG, response.getId().toString());
    }

    public UserData getUserData()
    {
        SharedPreferencesHandler handler = new SharedPreferencesHandler(context);
        Map<String,String> map = new HashMap<>();
        //map.put(Constants.USER_ID_TAG, handler.GetString(Constants.USER_ID_TAG));
        map.put(Constants.USER_NAME_TAG, handler.GetString(Constants.USER_NAME_TAG));
        JSONObject obj = new JSONObject(map);
        return handler.GetIt(obj.toString(),UserData.class);
    }

    public String getAccessToken()
    {
        SharedPreferencesHandler handler = new SharedPreferencesHandler(context);
        return handler.GetString(Constants.ACCESS_TOKEN_TAG);
    }
}