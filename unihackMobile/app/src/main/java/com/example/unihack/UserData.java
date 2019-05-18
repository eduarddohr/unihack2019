package com.example.unihack;

import com.google.gson.annotations.SerializedName;

import java.util.List;
import java.util.UUID;

public class UserData {

    @SerializedName("userName")
    public String FullName;

    /*
    @SerializedName("id")
    private UUID id;

    public List<IssueGetModel> Issues;

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }
    */
}
