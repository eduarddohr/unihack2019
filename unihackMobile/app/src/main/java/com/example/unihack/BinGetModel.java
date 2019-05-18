package com.example.unihack;

import java.util.List;
import java.util.UUID;

public class BinGetModel {

    public UUID Id;

    public String Name;

    public int Type;

    public float Latitude;

    public float Longitude;

    public float Capacity;

    public int Zone;

    public String ZoneName;

    public List<CollectorModel> Collectors;

    public String ManagerId;

    public String ManagerName;

    public BinGetModel(AddBinModel bin) {
        Name = bin.Name;
        Type = bin.Type;
        Latitude = bin.Latitude;
        Longitude = bin.Longitude;
        Capacity = bin.Capacity;
        Zone = bin.Zone;
    }
    public BinGetModel() {

    }
}
