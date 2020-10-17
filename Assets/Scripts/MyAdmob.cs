using UnityEngine;
using admob;
using UnityEngine.UI;


/// <summary>
/// Controls Admob ads
/// </summary>
public class MyAdmob : MonoBehaviour
{

    Admob ad;

    
    //string appID="";
    string bannerIDTop = "";
    string bannerIDBottom = "";

    string videoID = "";

    string interStitialID = "";

    //Reward on reward video..
    //score will increae by 1 each time when player watch rewarded ad.
    private int score = 0;

    //Displays score on UI
    public Text scoreText; 



    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        initAdmob();
    }



    /// <summary>
    /// Initializing admob
    /// </summary>
    void initAdmob()
    {
        //These all are test ads,
        //Please replace it with you ids..
        //Seperate Ids for IOS and Android
        

#if UNITY_IOS

        		// appID="ca-app-pub-3940256099942544~1458002511";
				 bannerIDTop="ca-app-pub-3940256099942544/2934735716";
				 bannerIDBottom="ca-app-pub-3940256099942544/2934735716";
				 interstitialID="ca-app-pub-3940256099942544/4411468910";
				 videoID="ca-app-pub-3940256099942544/1712485313";
				 nativeBannerID = "ca-app-pub-3940256099942544/3986624511";

#elif UNITY_ANDROID

        //appID="ca-app-pub-3940256099942544~3347511713";
        bannerIDTop = "ca-app-pub-3940256099942544/6300978111";
        bannerIDBottom = "ca-app-pub-3940256099942544/6300978111";
        videoID = "ca-app-pub-3940256099942544/5224354917";
        interStitialID = "ca-app-pub-3940256099942544/1033173712";

#endif
        AdProperties adProperties = new AdProperties();

        //NOTE: SET isTesting() TO FALSE WHEN NOT TESTING
        adProperties.isTesting(true);


        adProperties.isAppMuted(true);
        adProperties.isUnderAgeOfConsent(false);
        adProperties.appVolume(100);
        adProperties.maxAdContentRating(AdProperties.maxAdContentRating_G);
        string[] keywords = { "diagram", "league", "brambling" };
        adProperties.keyworks(keywords);

        ad = Admob.Instance();

        //Registering events..
        ad.bannerEventHandler += onBannerEvent;
        ad.rewardedVideoEventHandler += onRewardedVideoEvent;

        ad.initSDK(adProperties);
    }




    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        ShowTopBanner();
        ShowBottomBanner();

        //loading rewarded video so it will be available on clicking button
        LoadRewardedVideo();

        //loading interstitial ad so it will be available on clicking button
        LoadInterstitial();

    }


    /// <summary>
    /// Shows top banner 
    /// </summary>
    void ShowTopBanner()
    {
        ad.showBannerRelative(bannerIDTop, AdSize.SMART_BANNER, AdPosition.TOP_CENTER, 0, "topBanner");
    }


    /// <summary>
    /// Shows bottom banner
    /// </summary>
    void ShowBottomBanner()
    {
        ad.showBannerRelative(bannerIDBottom, AdSize.SMART_BANNER, AdPosition.BOTTOM_CENTER, 0, "bottomBanner");
    }




    /// <summary>
    /// Shows rewarded video 
    /// </summary>
    public void ShowRewardedVideo()
    {
        Debug.Log("pressed video button -------------");
        if (ad.isRewardedVideoReady())
        {
            ad.showRewardedVideo();
        }
        else
        {
            //you can show text on ui with message like "video is not available"
            LoadRewardedVideo();
        }
    }


    /// <summary>
    /// It will load rewarded video before showing,so video will be ready to show
    /// </summary>
    void LoadRewardedVideo()
    {
        ad.loadRewardedVideo(videoID);
    }



    /// <summary>
    /// It will load interstitial before showing, so interstitial will be ready to show
    /// </summary>
    void LoadInterstitial()
    {
        ad.loadInterstitial(interStitialID);
    }



    /// <summary>
    /// Shows interstitial ad
    /// </summary>
    public void ShowInterstitial()
    {
        Debug.Log("pressed video button -------------");
        if (ad.isInterstitialReady())
        {
            ad.showInterstitial();
        }else
        {
            Debug.Log("Interstitial not loaded yet!");
            LoadInterstitial();
        }
    }

   
    ///////////////////////////////////////////////////////////////////////////////
    ////CALLBACKS...
    ////////////////////////////////////////////////////////////////////////////////

    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }


    void onRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);

        //INCREMENT SCORE ONLY WHEN PLAYER WATCH FULL VIDEO
        if (eventName == AdmobEvent.onRewarded)
        {
            score++;
            scoreText.text = "SCORE : " + score.ToString();
        }

    }

}
