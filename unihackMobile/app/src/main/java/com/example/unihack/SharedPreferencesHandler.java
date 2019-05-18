package com.example.unihack;

import android.content.Context;
import android.content.SharedPreferences;

import com.google.gson.Gson;

import java.lang.reflect.Type;

public class SharedPreferencesHandler {
    private static final String SHARED_PREFERENCES_NAME = "data";
    private Context context;

    public SharedPreferencesHandler(Context context)
    {
        this.context = context;
    }

    public void SetString(String tag, String data)
    {
        SharedPreferences sh = context.getSharedPreferences(SHARED_PREFERENCES_NAME,Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sh.edit();
        editor.putString(tag,data);
        editor.apply();
    }

    public String GetString(String tag)
    {
        SharedPreferences sh = context.getSharedPreferences(SHARED_PREFERENCES_NAME,Context.MODE_PRIVATE);
        return sh.getString(tag, null);
    }

    public void Set(String tag, Object data)
    {
        Gson gson = new Gson();
        SetString(tag,gson.toJson(data));
    }

    public <T> T GetIt(String data, Class<T> classOfT){
        return new Gson().fromJson(data, classOfT);
    }
}
