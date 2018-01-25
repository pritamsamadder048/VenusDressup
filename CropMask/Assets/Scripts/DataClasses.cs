using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;




#region APPARELDATA

[Serializable]
public class DressData
{
    public string propertyType = "dress";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    public bool _isInitialized = false;
    public float[] pColor=new float[] { .5f, .5f, .5f, 1 };

    public void EncodeData(DressProperties p, Color pcolor)
    {

        wearingCode = p.wearingCode;
        mfType = p.mfType;
        imgName = p.imgName;
        finalImageUrl = p.finalImageUrl;
        finalSavePath = p.finalSavePath;
        lockStatus = p.lockStatus;
        serializedJsonObject = p.serializedJsonObject;
        _isInitialized = false;
        pColor = new float []{ pcolor.r, pcolor.g,pcolor.b,pcolor.a};
    }

    public void DecodeData(out DressProperties p)
    {
        p = new DressProperties
        {

            wearingCode = wearingCode,
            mfType = mfType,
            imgName = imgName,
            finalImageUrl = finalImageUrl,
            finalSavePath = finalSavePath,
            lockStatus = lockStatus,
            serializedJsonObject = serializedJsonObject,
        };
        
    }
}

[Serializable]
public class FemaleWigData
{
    public string propertyType = "wig";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    public bool _isInitialized = false;
    public float[] pColor = new float[] { .5f, .5f, .5f, 1f };

    public void EncodeData(FemaleWigProperties p, Color pcolor)
    {
        wearingCode = p.wearingCode;
        mfType = p.mfType;
        imgName = p.imgName;
        finalImageUrl = p.finalImageUrl;
        finalSavePath = p.finalSavePath;
        lockStatus = p.lockStatus;
        serializedJsonObject = p.serializedJsonObject;
        _isInitialized = false;
        pColor = new float[] { pcolor.r, pcolor.g, pcolor.b, pcolor.a };
    }

    public void DecodeData(out FemaleWigProperties p)
    {
        p = new FemaleWigProperties
        {
            wearingCode = wearingCode,
            mfType = mfType,
            imgName = imgName,
            finalImageUrl = finalImageUrl,
            finalSavePath = finalSavePath,
            lockStatus = lockStatus,
            serializedJsonObject = serializedJsonObject
            
    };
        
    }
}

[Serializable]
public class OrnamentData
{
    public string propertyType = "ornament";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    public bool _isInitialized = false;

    public void EncodeData(OrnamentProperties p)
    {
        wearingCode = p.wearingCode;
        mfType = p.mfType;
        imgName = p.imgName;
        finalImageUrl = p.finalImageUrl;
        finalSavePath = p.finalSavePath;
        lockStatus = p.lockStatus;
        serializedJsonObject = p.serializedJsonObject;
        _isInitialized = false;
    }

    public void DecodeData(out OrnamentProperties p)
    {
        p = new OrnamentProperties
        {
            wearingCode = wearingCode,
            mfType = mfType,
            imgName = imgName,
            finalImageUrl = finalImageUrl,
            finalSavePath = finalSavePath,
            lockStatus = lockStatus,
            serializedJsonObject = serializedJsonObject
        };

    }
}

[Serializable]
public class ShoeData
{
    public string propertyType = "shoe";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    public bool _isInitialized = false;

    public void EncodeData(ShoeProperties p)
    {
        
        wearingCode = p.wearingCode;
        mfType = p.mfType;
        imgName = p.imgName;
        finalImageUrl = p.finalImageUrl;
        finalSavePath = p.finalSavePath;
        lockStatus = p.lockStatus;
        serializedJsonObject = p.serializedJsonObject;
        _isInitialized = false;
    }

    public void DecodeData(out ShoeProperties p)
    {
        p = new ShoeProperties
        {
            wearingCode = wearingCode,
            mfType = mfType,
            imgName = imgName,
            finalImageUrl = finalImageUrl,
            finalSavePath = finalSavePath,
            lockStatus = lockStatus,
            serializedJsonObject = serializedJsonObject
        };


    }
}



[Serializable]
public class BackGroundData
{
    public string propertyType = "background";

    public string backGroundName;
    public string backGroundPath;

    public void EncodeData(BackGroundProperty bp)
    {

        backGroundName = bp.backGroundName;
        backGroundPath = bp.backGroundPath;
    }

    public void DecodeData(out BackGroundProperty bp)
    {
        bp = new BackGroundProperty
        {
            backGroundName = backGroundName,
            backGroundPath = backGroundPath
        };


    }
}


[Serializable]
public class MaleData
{
    public string propertyType = "male";
    public bool maleIsShowing = false;

    public string maleName;
    public int maleIndex;
    public float maleCarouselRotation;

    public string maleWigName;
    public string finalMaleWigImageUrl;
    public string finalMaleWigSavePath;
    public string serializedMaleWigProperty;

    public string maleTieName;
    public string finalMaleTieImageUrl;
    public string finalMaleTieSavePath;
    public string serializedMaleTieProperty;

    public float[] maleWigColor;
    public float[] maleTieColor;

    public bool isWearingWig;
    public bool isWearingTie;

    public void EncodeData(GameController gameController)
    {
        if (gameController.isShowingMale)
        {
            maleIsShowing = true;
            maleName = gameController.mainMaleBodyName;
            maleIndex = gameController.maleController.currentMaleIndex;
            maleCarouselRotation = gameController.maleController.currentCarouselRotation;
            try
            {
                if (gameController.maleController.isWearingWig&&gameController.currentMaleWigProperty.imgName != "" && gameController.currentMaleWigProperty.imgName != null)
                {
                    maleWigName = gameController.currentMaleWigProperty.imgName;
                    finalMaleWigImageUrl = gameController.currentMaleWigProperty.finalImageUrl;
                    finalMaleWigSavePath = gameController.currentMaleWigProperty.finalSavePath;
                    serializedMaleWigProperty = gameController.currentMaleWigProperty.serializedJsonObject;
                    maleWigColor = new float[] {gameController.maleWig.color.r, gameController.maleWig.color.g, gameController.maleWig.color.b, gameController.maleWig.color.a };
                    isWearingWig = true;
                }
                else
                {
                    isWearingWig = false;
                }
            }
            catch (Exception e)
            {
                isWearingWig = false;
                Debug.Log("male wig error : " + e.Message);
            }

            try
            {
                if (gameController.maleController.isWearingTie && gameController.currentMaleTieProperty.imgName != "" && gameController.currentMaleTieProperty.imgName != null)
                {
                    maleTieName = gameController.currentMaleTieProperty.imgName;
                    finalMaleTieImageUrl = gameController.currentMaleTieProperty.finalImageUrl;
                    finalMaleTieSavePath = gameController.currentMaleTieProperty.finalSavePath;
                    serializedMaleTieProperty = gameController.currentMaleTieProperty.serializedJsonObject;
                    maleTieColor = new float[] { gameController.maleTie.color.r, gameController.maleTie.color.g, gameController.maleTie.color.b, gameController.maleTie.color.a };
                    isWearingTie = true;
                }
                else
                {
                    isWearingTie = false;
                }
            }
            catch (Exception e)
            {
                isWearingTie = false;
                Debug.Log("male tie error : " + e.Message);
            }
        }
        else
        {
            maleIsShowing = false;
        }
    }

    

    public void DecodeData(out MaleWigProperties maleWigProperty,out MaleTieProperties maleTieProperty,out bool malePresent)
    {
        malePresent = maleIsShowing;
        if(maleIsShowing)
        {
            if(isWearingWig)
            {
                maleWigProperty = new MaleWigProperties
                {
                     wearingCode=5,
                     mfType = 2,
                     imgName=maleWigName,
                     finalImageUrl=finalMaleWigImageUrl,
                     finalSavePath=finalMaleWigSavePath,
                     serializedJsonObject = serializedMaleWigProperty,
                };
            }
            else
            {
                maleWigProperty = null;
            }


            if (isWearingTie)
            {
                maleTieProperty = new MaleTieProperties
                {
                    wearingCode = 6,
                    mfType = 2,
                    imgName = maleTieName,
                    finalImageUrl = finalMaleTieImageUrl,
                    finalSavePath = finalMaleTieSavePath,
                    serializedJsonObject = serializedMaleTieProperty,
                };
            }
            else
            {
                maleTieProperty = null;
            }
        }
        else
        {
            maleWigProperty = null;
            maleTieProperty = null;
            return;
        }
    }
}
#endregion APPARELDATA

#region SAVEDATA
[Serializable]
public class SaveData
{
    public string saveName;
    public int modelIndex;
    public float modelRotation;
    public string modelNameame;
    public string bodytoneName;
    public string eyeName;
    public string dressName;
    public string wigName;
    public string ornamentName;
    public string shoeName;
    public string backgroundName;
    public bool isShowingMale=false;

    [NonSerialized]
    public DressProperties dressProperty;
    [NonSerialized]
    public FemaleWigProperties femaleWigProperty;
    [NonSerialized]
    public OrnamentProperties ornamentProperty;
    [NonSerialized]
    public ShoeProperties shoeProperty;
    [NonSerialized]
    public BackGroundProperty backgroundProperty;
    [NonSerialized]
    public MaleWigProperties maleWigProperty;
    [NonSerialized]
    public MaleTieProperties maleTieProperty;

    public DressData dressData;
    public FemaleWigData femaleWigData;
    public OrnamentData ornamentData;
    public ShoeData shoeData;
    public BackGroundData backgroundData;

    public MaleData maleData;
    


    private void Initialize(int modelindex,float modelrotation,string model,string bodytone,string eye,GameController gameController, string dress="", string wig="", string ornament="", string shoe="")
    {
        dressData = new DressData();
        femaleWigData = new FemaleWigData();
        ornamentData = new OrnamentData();
        shoeData = new ShoeData();
        backgroundData = new BackGroundData();

        modelindex = modelIndex;
        modelRotation = modelrotation;
        modelNameame = model;
        bodytoneName = bodytone;
        eyeName = eye;
        dressName = dress;
        wigName = wig;
        ornamentName = ornament;
        shoeName = shoe;
        gameController.currentBackgroundProperty.CopyTo(ref backgroundProperty);
        backgroundName = backgroundProperty.backGroundName;
        isShowingMale = gameController.isShowingMale;
        
    }

    public void Initialize(int modelindex,float modelrotation, string model, string bodytone, string eye, GameController gameController, DressProperties dressproperty = null, FemaleWigProperties wigproperty = null, OrnamentProperties ornamentproperty = null, ShoeProperties shoeproperty = null)
    {
        dressData = new DressData();
        femaleWigData = new FemaleWigData();
        ornamentData = new OrnamentData();
        shoeData = new ShoeData();
        backgroundData = new BackGroundData();
        maleData = new MaleData();
        modelIndex = modelindex;
        modelRotation = modelrotation;
        modelNameame = model;
        bodytoneName = bodytone;
        eyeName = eye;
        this.dressProperty = dressproperty;
        this.femaleWigProperty = wigproperty;
        this.ornamentProperty = ornamentproperty;
        this.shoeProperty = shoeproperty;
        gameController.currentBackgroundProperty.CopyTo(ref backgroundProperty);

        
        //gameController.currentBackgroundProperty.CopyTo(ref backgroundProperty);
        isShowingMale = gameController.isShowingMale;

        if (gameController.isShowingMale)
        {

            maleData.maleIsShowing = true;
            maleWigProperty = gameController.currentMaleWigProperty;
            maleTieProperty = gameController.currentMaleTieProperty;

            maleData.EncodeData(gameController);

        }
        else
        {
            maleData.maleIsShowing = false;
        }
        if (dressProperty!=null)
        {
            
            dressData.EncodeData(dressProperty,gameController.currentDressColor);

            dressName = dressProperty.imgName;
        }
        if(femaleWigProperty!=null)
        {
            
            femaleWigData.EncodeData(femaleWigProperty,gameController.currentWigColor);

            wigName = femaleWigProperty.imgName;
        }
        if(ornamentProperty!=null)
        {
            
            
            ornamentData.EncodeData(ornamentProperty);

            ornamentName = ornamentProperty.imgName;
        }
        if(shoeProperty!=null)
        {

            shoeData.EncodeData(shoeProperty);

            shoeName = shoeProperty.imgName;
        }
        backgroundData.EncodeData(backgroundProperty);

    }

    public void ReCheckData(GameController gameController)
    {

        dressData = new DressData();
        femaleWigData = new FemaleWigData();
        ornamentData = new OrnamentData();
        shoeData = new ShoeData();
        backgroundData = new BackGroundData();
        maleData = new MaleData();
        gameController.currentBackgroundProperty.CopyTo(ref backgroundProperty);
        maleData.maleIsShowing=isShowingMale = gameController.isShowingMale;

        try
        {

            if (dressProperty != null || dressProperty.imgName != "" || dressProperty.imgName != null)
            {

                dressData.EncodeData(dressProperty, gameController.currentDressColor);
                Debug.Log(string.Format("Dress color is {0} {1} {2} {3}", dressData.pColor[0], dressData.pColor[1], dressData.pColor[2], dressData.pColor[3]));
                dressName = dressProperty.imgName;
            }

        }
        catch (Exception e)
        {
            Debug.Log(string.Format("Dress recheck Error : {0}",e.Message));
            
        }

        try
        {
            if (femaleWigProperty != null || femaleWigProperty.imgName != "" || femaleWigProperty.imgName != null)
            {

                femaleWigData.EncodeData(femaleWigProperty, gameController.currentWigColor);

                wigName = femaleWigProperty.imgName;
            }
        }
        catch (Exception e)
        {

            Debug.Log(string.Format("Wig rechec Error : {0}", e.Message));
        }

        try
        {
            if (ornamentProperty != null || ornamentProperty.imgName != "" || ornamentProperty.imgName != null)
            {


                ornamentData.EncodeData(ornamentProperty);

                ornamentName = ornamentProperty.imgName;
            }
        }
        catch (Exception e)
        {

            Debug.Log(string.Format("Ornament rechec Error : {0}", e.Message));

        }

        try
        {
            if (shoeProperty != null || shoeProperty.imgName != "" || shoeProperty.imgName != null)
            {

                shoeData.EncodeData(shoeProperty);

                shoeName = shoeProperty.imgName;
            }
        }
        catch (Exception e)
        {

            Debug.Log(string.Format("Shoe recheck Error : {0}", e.Message));
        }


        maleData.EncodeData(gameController);

        backgroundData.EncodeData(backgroundProperty);
        //Debug.Log(string.Format("Recheck data : background name :{0}  background path {1} ", backgroundData.backGroundName, backgroundData.backGroundPath));
        Debug.Log(string.Format("male data : male showing : {0}  Wearing wig : {1}  wearing tie : {2}", maleData.maleIsShowing, maleData.isWearingWig, maleData.isWearingTie));

    }

    public void InverseRecheckData()
    {
        dressData.DecodeData(out this.dressProperty);
        dressProperty.InitializeDressProperty(dressData.serializedJsonObject);
        femaleWigData.DecodeData(out this.femaleWigProperty);
        ornamentData.DecodeData(out this.ornamentProperty);
        shoeData.DecodeData(out this.shoeProperty);
        backgroundData.DecodeData(out this.backgroundProperty);
        maleData.DecodeData(out this.maleWigProperty, out this.maleTieProperty, out this.isShowingMale);
    }
    public void InitializeProperties()
    {
        dressProperty = new DressProperties();
        dressProperty.InitializeDressProperty(dressData.serializedJsonObject);
        dressProperty.SetDressColor(dressData.pColor);
        femaleWigProperty = new FemaleWigProperties();
        femaleWigProperty.InitializeWigProperty(femaleWigData.serializedJsonObject);
        femaleWigProperty.SetFemaleWigColor(femaleWigData.pColor);
        ornamentProperty = new OrnamentProperties();
        ornamentProperty.InitializeOrnamentProperty(ornamentData.serializedJsonObject);
        shoeProperty = new ShoeProperties();
        shoeProperty.InitializeShoeProperty(shoeData.serializedJsonObject);

        backgroundProperty = new BackGroundProperty();
        backgroundData.DecodeData(out backgroundProperty);

        Debug.Log(string.Format(" InitializeProperties-- background data : {0}....{1}", backgroundData.backGroundName, backgroundData.backGroundPath));
        Debug.Log(string.Format(" InitializeProperties-- background Properties : {0}....{1}", backgroundProperty.backGroundName, backgroundProperty.backGroundPath));

    }
    public static int SaveWearings(string saveDataFileName, SaveData sd,GameController gc)
    {


        try
        {

            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/wearingsdata";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, saveDataFileName);
            MonoBehaviour.print(string.Format("Final Save path : {0}", finalSavePath));
            if (File.Exists(finalSavePath))
            {
                MonoBehaviour.print("file exist,saving data");
                List<SaveData> saveDatas = new List<SaveData>();
                saveDatas = LoadData(saveDataFileName);

                int totalSaves = 0; 
                if(saveDatas!=null)
                {
                    totalSaves=saveDatas.Count;
                }
                else
                {
                    saveDatas = new List<SaveData>();
                }
                MonoBehaviour.print(string.Format("total saves :{0}", totalSaves));

                if(totalSaves>=10)
                {
                    return 0;
                }


                if (totalSaves < 10)
                {
                    FileStream fileStream;
                    fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();
                    string save_name = string.Format("savedata_{0}.jpg", (int)(UnityEngine.Random.Range(0, 9999)));
                    sd.saveName = save_name;
                    saveDatas.Add(sd);
                    MonoBehaviour.print(string.Format("total saves : {0}", saveDatas.Count));

                    b.Serialize(fileStream, saveDatas);
                    fileStream.Flush();
                    fileStream.Close();
                    string saveImageName = Path.Combine(dataPath, "screenshots");
                    if (!Directory.Exists(saveImageName))
                    {
                        Directory.CreateDirectory(saveImageName);
                    }
                    saveImageName = Path.Combine(saveImageName, save_name);








#if UNITY_EDITOR
                    MonoBehaviour.print(string.Format("Editor saving screenshot to : {0}", saveImageName));
                    gc.CallBackFromSaveWearings(saveImageName);
#elif UNITY_ANDROID

                        string tsn = string.Format("wearingsdata/screenshots/{0}", save_name);
                        MonoBehaviour.print(string.Format("Android saving screenshot to : {0}", tsn));
                        gc.CallBackFromSaveWearings(tsn);
#elif UNITY_IPHONE

                    string tsn = string.Format("wearingsdata/screenshots/{0}", save_name);
                    Debug.Log(string.Format("Iphone saving screenshot to : {0}", tsn));
                    gc.CallBackFromSaveWearings(tsn);

#endif
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                MonoBehaviour.print("Does Not Exist , Creating new File");
                List<SaveData> saveDatas = new List<SaveData>();

                MonoBehaviour.print(string.Format(" total saves :{0}", saveDatas.Count));
                string save_name = string.Format("savedata_{0}.jpg", (int)(UnityEngine.Random.Range(0, 9999)));
                sd.saveName = save_name;
                saveDatas.Add(sd);

                FileStream fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();

                b.Serialize(fileStream, saveDatas);

                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();
                string saveImageName = Path.Combine(dataPath, "screenshots");
                if (!Directory.Exists(saveImageName))
                {
                    Directory.CreateDirectory(saveImageName);
                }
                saveImageName = Path.Combine(saveImageName, save_name);


#if UNITY_EDITOR
                MonoBehaviour.print(string.Format("Editor saving screenshot to : {0}", saveImageName));
                gc.CallBackFromSaveWearings(saveImageName);


#elif UNITY_ANDROID

                    string tsn = string.Format("wearingsdata/screenshots/{0}", save_name);
                    MonoBehaviour.print(string.Format("Android saving screenshot to : {0}", tsn));
                    gc.CallBackFromSaveWearings(tsn);
#elif UNITY_IPHONE

                    string tsn = string.Format("wearingsdata/screenshots/{0}", save_name);
                    MonoBehaviour.print(string.Format("Android saving screenshot to : {0}", tsn));
                    gc.CallBackFromSaveWearings(tsn);

#endif



                return 1;


            }

            
        }
        catch (Exception e)
        {
            MonoBehaviour.print("error : " + e);
            return -1;
        }


    }

    public static int SaveLooksFromData(List<SaveData> sd, string saveFileName)
    {
        try
        {

            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/wearingsdata";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, saveFileName);
            MonoBehaviour.print(string.Format("Final Save path : {0}", finalSavePath));

            if (File.Exists(finalSavePath))
            {
                MonoBehaviour.print("file exist,saving data");



                int totalScavedlooks = sd.Count;
                MonoBehaviour.print(string.Format("total faces :{0}", totalScavedlooks));



                if (totalScavedlooks <= 10)
                {
                    FileStream fileStream;
                    fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();


                    b.Serialize(fileStream, sd);
                    fileStream.Flush();
                    fileStream.Close();
                    MonoBehaviour.print("face data is saved");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                MonoBehaviour.print("Does Not Exist , Creating new File for saving data from list");


                MonoBehaviour.print(string.Format(" total faces :{0}", sd.Count));


                FileStream fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();

                b.Serialize(fileStream, sd);

                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();

                return 1;


            }
        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("Error occured when trying to save data from list : {0}", e));
            return -1;
        }
    }


    public static List<SaveData> DeleteSavedLook(SaveData s, List<SaveData> sd, string saveFileName)
    {
        List<SaveData> origList = sd;
        try
        {


            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/wearingsdata";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalImagePath = Path.Combine(dataPath, "screenshots");
            if (!Directory.Exists(finalImagePath))
            {
                Directory.CreateDirectory(finalImagePath);
            }
            finalImagePath = Path.Combine(finalImagePath, s.saveName);

            sd.Remove(s);
            if (File.Exists(finalImagePath))
            {
                MonoBehaviour.print(string.Format("Deleting : {0} as it exist", finalImagePath));
                File.Delete(finalImagePath);
                MonoBehaviour.print("file deleted successfully");
            }
            SaveLooksFromData(sd, saveFileName);

            return sd;
        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("Error occured when trying to save data from list : {0}", e));
            return origList;
        }
    }

    public static List<SaveData> LoadData(string fileName)
    {
        FileStream fileStream;
        try
        {
            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/wearingsdata";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, fileName);

            if (File.Exists(finalSavePath))
            {
                MonoBehaviour.print(string.Format("file exist : {0} ", finalSavePath));
                fileStream = new FileStream(finalSavePath, FileMode.Open, FileAccess.Read);
                try
                {
                    List<SaveData> saveDatas = new List<SaveData>();

                    BinaryFormatter b = new BinaryFormatter();
                    saveDatas = (List<SaveData>)b.Deserialize(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();
                    if(saveDatas!=null)
                    {
                        if(saveDatas.Count>0)
                        {
                            for(int i=0;i<saveDatas.Count;i++)
                            {
                                saveDatas[i].InitializeProperties();
                                Debug.Log(string.Format("savedata[{0}].dressproperty.dresscolor is : {1} {2} {3} {4} ",i, saveDatas[i].dressProperty.dressColor[0], saveDatas[i].dressProperty.dressColor[1], saveDatas[i].dressProperty.dressColor[2], saveDatas[i].dressProperty.dressColor[3]));
                                Debug.Log(string.Format("savedata[{0}].dressdata.pcolor is : {1} {2} {3} {4} ",i, saveDatas[i].dressData.pColor[0], saveDatas[i].dressData.pColor[1], saveDatas[i].dressData.pColor[2], saveDatas[i].dressData.pColor[3]));

                            }
                        }
                    }
                    return saveDatas;
                }
                catch (Exception e)
                {
                    MonoBehaviour.print(string.Format("error loading save file : {0} ", e));
                    fileStream.Close();
                    fileStream.Dispose();
                    return null;
                }

            }
            else
            {
                throw new FileNotFoundException(string.Format("file : {0} Does Not Exist", finalSavePath));


            }

        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("error occured loading save file : {0}", e));
            return null;
        }

    }


}
#endregion SAVEDATA

#region CROPPEDFACEDATA
[Serializable]
public class CroppedFaceData
{

    //public Byte[] croppedFaceImage;
    public string imageName;
    public float[] scale;
    public float[] sizeDelta;
    public float[] position;
    public float[] rotation;
    public float[] faceColor;
    public int saveFaceHash;



    public void Copy(ref CroppedFaceData cd)
    {
        cd = new CroppedFaceData();

        cd.imageName = this.imageName;
        cd.saveFaceHash = this.saveFaceHash;
        cd.scale = new float[scale.Length];
        cd.sizeDelta = new float[sizeDelta.Length];
        cd.position = new float[position.Length];
        cd.rotation = new float[rotation.Length];
        cd.faceColor = new float[faceColor.Length];



        for(int i=0;i<scale.Length;i++)
        {
            cd.scale[i] = this.scale[i];
        }

        for (int i = 0; i < sizeDelta.Length; i++)
        {
            cd.sizeDelta[i] = this.sizeDelta[i];
        }

        for (int i = 0; i < position.Length; i++)
        {
            cd.position[i] = this.position[i];
        }

        for (int i = 0; i < rotation.Length; i++)
        {
            cd.rotation[i] = this.rotation[i];
        }

        for (int i = 0; i < faceColor.Length; i++)
        {
            cd.faceColor[i] = this.faceColor[i];
        }
    }

    public void Initialize( Vector3 scl,Vector2 size_delta,Vector3 pos,Vector3 rot,Color col)
    {
        
        //croppedFaceImage = faceTexture.EncodeToPNG();
        
        sizeDelta =new float[] { size_delta.x,size_delta.y};
        position = new float[] { pos.x, pos.y, pos.z };
        rotation = new float[] { rot.x, rot.y, rot.z };
        scale = new float[] { scl.x, scl.y, scl.z };
        faceColor = new float[] { col.r, col.g, col.b, col.a };

        saveFaceHash = GetHashCode()*UnityEngine.Random.Range(2,10);
    }

    public void Initialize(RawImage img)
    {
        
        //croppedFaceImage =((Texture2D) img.texture).EncodeToPNG();
        
        sizeDelta = new float[] { img.GetComponent<RectTransform>().sizeDelta.x, img.GetComponent<RectTransform>().sizeDelta.y };
        scale = new float[] { img.transform.localScale.x, img.transform.localScale.y, img.transform.localScale.z };
        position = new float[] { img.GetComponent<RectTransform>().anchoredPosition3D.x, img.GetComponent<RectTransform>().anchoredPosition3D.y, img.GetComponent<RectTransform>().anchoredPosition3D.z };
        rotation = new float[] { img.transform.localEulerAngles.x, img.transform.localEulerAngles.y, img.transform.localEulerAngles.z };
        faceColor = new float[] { img.color.r, img.color.g, img.color.b, img.color.a };
        saveFaceHash = GetHashCode() * UnityEngine.Random.Range(2, 10);


    }

    public static int SaveCroppedFaceDict(string saveDataFileName,CroppedFaceData cfd,RawImage rwimg)
    {
        
        
        try
        {
            MonoBehaviour.print(Application.persistentDataPath);
            MonoBehaviour.print(Application.persistentDataPath);
            string dataPath = Application.persistentDataPath;
            
            if(Application.platform!=RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
            string finalSavePath = Path.Combine(dataPath, saveDataFileName);
            MonoBehaviour.print(string.Format("Final Save path : {0}", finalSavePath));
            if(File.Exists(finalSavePath))
            {
                MonoBehaviour.print("file exist,saving data");
                Dictionary<string, CroppedFaceData> croppedFaceFiles = new Dictionary<string, CroppedFaceData>();
                croppedFaceFiles = LoadDataDict(saveDataFileName); ;

                int totalScavedFaces = croppedFaceFiles.Count;
                MonoBehaviour.print(string.Format("total faces :{0}",totalScavedFaces ));
                
                foreach(string s in croppedFaceFiles.Keys)
                {
                    MonoBehaviour.print(string.Format("loaded cropped face file : {0}", s));
                }

                if(totalScavedFaces<5)
                {
                    FileStream fileStream;
                    fileStream = new FileStream(finalSavePath, FileMode.Open, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();
                    string save_name = string.Format("croppedface_{0}", croppedFaceFiles.Count);
                    croppedFaceFiles.Add(save_name, cfd);
                    MonoBehaviour.print(string.Format("total faces : {0}", croppedFaceFiles.Count));
                    foreach (string s in croppedFaceFiles.Keys)
                    {
                        MonoBehaviour.print(string.Format("writing cropped face file : {0}", s));
                    }
                    b.Serialize(fileStream, croppedFaceFiles);
                    fileStream.Flush();
                    fileStream.Close();
                    string saveImageName = Path.Combine(dataPath, "faceimages");
                    saveImageName = Path.Combine(saveImageName, save_name + "png");
                    File.WriteAllBytes(saveImageName,(rwimg.texture as Texture2D).EncodeToPNG());
                    MonoBehaviour.print("face is saved");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                MonoBehaviour.print("Does Not Exist , Creating new File");
                Dictionary<string, CroppedFaceData> croppedFaceFiles = new Dictionary<string, CroppedFaceData>();

                MonoBehaviour.print(string.Format(" total faces :{0}",croppedFaceFiles.Count));
                string save_name = string.Format("croppedface_{0}", croppedFaceFiles.Count);
                croppedFaceFiles.Add(save_name, cfd);
                foreach (string s in croppedFaceFiles.Keys)
                {
                    MonoBehaviour.print(string.Format("writing first cropped face file : {0}", s));
                }
                FileStream fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();

                b.Serialize(fileStream, croppedFaceFiles);
                
                fileStream.Flush();
                fileStream.Close();
                string saveImageName = Path.Combine(dataPath, "faceimages");
                saveImageName = Path.Combine(saveImageName, save_name + "png");
                File.WriteAllBytes(saveImageName, (rwimg.texture as Texture2D).EncodeToPNG());
                MonoBehaviour.print("face is saved");
                return 1;


            }
            
        }
        catch(Exception e)
        {
            MonoBehaviour.print("error : " + e);
            return -1;
        }
    }

    public static Dictionary<string, CroppedFaceData> LoadDataDict(string fileName)
    {
        try
        {
            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
                string finalSavePath = Path.Combine(dataPath, fileName);

                if(File.Exists(finalSavePath))
                {
                    Dictionary<string, CroppedFaceData> croppedFaceFiles = new Dictionary<string, CroppedFaceData>();
                    FileStream fileStream = new FileStream(finalSavePath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter b = new BinaryFormatter();
                    croppedFaceFiles = (Dictionary<string, CroppedFaceData>)b.Deserialize(fileStream);
                    return croppedFaceFiles;
                }
                else
                {
                    throw new FileNotFoundException(string.Format("file : {0} Does Not Exist",finalSavePath));
                    
                }
            
        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("error occured loading save file : {0}", e));
            return null;
        }
    }



    public static int SaveCroppedFace(string saveDataFileName, CroppedFaceData cfd, RawImage rwimg)
    {


        try
        {
            MonoBehaviour.print(Application.persistentDataPath);
            MonoBehaviour.print(Application.persistentDataPath);
            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, saveDataFileName);
            MonoBehaviour.print(string.Format("Final Save path : {0}", finalSavePath));
            if (File.Exists(finalSavePath))
            {
                MonoBehaviour.print("file exist,saving data");
                List<CroppedFaceData> croppedFaceFiles = new List<CroppedFaceData>();
                croppedFaceFiles = LoadData(saveDataFileName); 

                int totalScavedFaces = croppedFaceFiles.Count;
                MonoBehaviour.print(string.Format("total faces :{0}", totalScavedFaces));

                

                if (totalScavedFaces < 5)
                {
                    FileStream fileStream;
                    fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();
                    string save_name = string.Format("croppedface_{0}.png", (int)(UnityEngine.Random.Range(0,9999)));
                    cfd.imageName = save_name;
                    croppedFaceFiles.Add( cfd);
                    MonoBehaviour.print(string.Format("total faces : {0}", croppedFaceFiles.Count));
                    
                    b.Serialize(fileStream, croppedFaceFiles);
                    fileStream.Flush();
                    fileStream.Close();
                    string saveImageName = Path.Combine(dataPath, "faceimages");
                    if (!Directory.Exists(saveImageName))
                    {
                        Directory.CreateDirectory(saveImageName);
                    }
                    saveImageName = Path.Combine(saveImageName, save_name);
                    File.WriteAllBytes(saveImageName, (rwimg.texture as Texture2D).EncodeToPNG());
                    MonoBehaviour.print("face is saved");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                MonoBehaviour.print("Does Not Exist , Creating new File");
                List<CroppedFaceData> croppedFaceFiles = new List<CroppedFaceData>();

                MonoBehaviour.print(string.Format(" total faces :{0}", croppedFaceFiles.Count));
                string save_name = string.Format("croppedface_{0}.png", (int)(UnityEngine.Random.Range(0, 9999)));
                cfd.imageName = save_name;
                croppedFaceFiles.Add(cfd);
                
                FileStream fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();

                b.Serialize(fileStream, croppedFaceFiles);

                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();
                string saveImageName = Path.Combine(dataPath, "faceimages");
                if (!Directory.Exists(saveImageName))
                {
                    Directory.CreateDirectory(saveImageName);
                }
                saveImageName = Path.Combine(saveImageName, save_name);
                File.WriteAllBytes(saveImageName, (rwimg.texture as Texture2D).EncodeToPNG());
                MonoBehaviour.print("face is saved");

                return 1;


            }

        }
        catch (Exception e)
        {
            MonoBehaviour.print("error : " + e);
            return -1;
        }


    }

    public static List<CroppedFaceData> DeleteCroppedFace(CroppedFaceData c, List<CroppedFaceData> cd, string saveFileName)
    {
        List<CroppedFaceData> origList = cd;
        try
        {
            

            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalImagePath = Path.Combine(dataPath, "faceimages");
            if (!Directory.Exists(finalImagePath))
            {
                Directory.CreateDirectory(finalImagePath);
            }
            finalImagePath = Path.Combine(finalImagePath, c.imageName);
            
            cd.Remove(c);
            if (File.Exists(finalImagePath))
            {
                MonoBehaviour.print(string.Format("Deleting : {0} as it exist", finalImagePath));
                File.Delete(finalImagePath);
                MonoBehaviour.print("file deleted successfully");
            }
            SaveCroppedFaceFromData(cd, saveFileName);

            return cd;
        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("Error occured when trying to save data from list : {0}", e));
            return origList ;
        }
    }
    public static int SaveCroppedFaceFromData(List<CroppedFaceData> cd, string saveFileName)
    {
        try
        {

            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, saveFileName);
            MonoBehaviour.print(string.Format("Final Save path : {0}", finalSavePath));
            
            if (File.Exists(finalSavePath))
            {
                MonoBehaviour.print("file exist,saving data");



                int totalScavedFaces = cd.Count;
                MonoBehaviour.print(string.Format("total faces :{0}", totalScavedFaces));



                if (totalScavedFaces <= 5)
                {
                    FileStream fileStream;
                    fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();


                    b.Serialize(fileStream, cd);
                    fileStream.Flush();
                    fileStream.Close();
                    MonoBehaviour.print("face data is saved");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                MonoBehaviour.print("Does Not Exist , Creating new File for saving data from list");


                MonoBehaviour.print(string.Format(" total faces :{0}", cd.Count));


                FileStream fileStream = new FileStream(finalSavePath, FileMode.Create, FileAccess.Write);
                BinaryFormatter b = new BinaryFormatter();

                b.Serialize(fileStream, cd);

                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();

                return 1;


            }
        }
        catch(Exception e)
        {
            MonoBehaviour.print(string.Format("Error occured when trying to save data from list : {0}", e));
            return -1;
        }
    }

    public static List<CroppedFaceData> LoadData(string fileName)
    {
        FileStream fileStream;
        try
        {
            string dataPath = Application.persistentDataPath;

            if (Application.platform != RuntimePlatform.OSXPlayer)
            {
                dataPath += "/croppedfaces";
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string finalSavePath = Path.Combine(dataPath, fileName);

            if (File.Exists(finalSavePath))
            {
                fileStream = new FileStream(finalSavePath, FileMode.Open, FileAccess.Read);
                try
                {
                    List<CroppedFaceData> croppedFaceFiles = new List<CroppedFaceData>();
                    
                    BinaryFormatter b = new BinaryFormatter();
                    croppedFaceFiles = (List<CroppedFaceData>)b.Deserialize(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();
                    return croppedFaceFiles;
                }
                catch(Exception e)
                {
                    MonoBehaviour.print(string.Format("error writting file : {0} ", e));
                    fileStream.Close();
                    fileStream.Dispose();
                    return null;
                }
                
            }
            else
            {
                throw new FileNotFoundException(string.Format("file : {0} Does Not Exist", finalSavePath));


            }

        }
        catch (Exception e)
        {
            MonoBehaviour.print(string.Format("error occured loading save file : {0}", e));
            return null;
        }
        
    }

    
}

#endregion CROPPEDFACEDATA




