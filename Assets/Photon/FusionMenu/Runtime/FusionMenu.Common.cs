// merged Menu

#region FusionMenuConnectArgs.cs

namespace Fusion.Menu {
  using UnityEngine;

  /// <summary>
  /// The connection options selected by the client which are operated on directly from <see cref="PlayerPrefs"/>.
  /// The menu screens all have the same instance of this object.
  /// </summary>
  public partial class FusionMenuConnectArgs {
    /// <summary>
    /// The username configured in the menu.
    /// </summary>
    public virtual string Username {
      get => PlayerPrefs.GetString("Photon.Menu.Username");
      set => PlayerPrefs.SetString("Photon.Menu.Username", value);
    }

    /// <summary>
    /// The session that the client wants to join. Is not persisted. Use ReconnectionInformation instead to recover it between application shutdowns.
    /// </summary>
    public virtual string Session { get; set; }

    /// <summary>
    /// The preferred region the user selected in the menu.
    /// </summary>
    public virtual string PreferredRegion {
      get => PlayerPrefs.GetString("Photon.Menu.Region");
      set => PlayerPrefs.SetString("Photon.Menu.Region", string.IsNullOrEmpty(value) ? value : value.ToLower());
    }

    /// <summary>
    /// The actual region that the client will connect to.
    /// </summary>
    public virtual string Region { get; set; }

    /// <summary>
    /// The app version used for the Photon connection.
    /// </summary>
    public virtual string AppVersion {
      get => PlayerPrefs.GetString("Photon.Menu.AppVersion");
      set => PlayerPrefs.SetString("Photon.Menu.AppVersion", value);
    }

    /// <summary>
    /// The max player count that the user selected in the menu.
    /// </summary>
    public virtual int MaxPlayerCount {
      get => PlayerPrefs.GetInt("Photon.Menu.MaxPlayerCount");
      set => PlayerPrefs.SetInt("Photon.Menu.MaxPlayerCount", value);
    }

    /// <summary>
    /// The map or scene information that the user selected in the menu.
    /// </summary>
    public virtual PhotonMenuSceneInfo Scene {
      get {
        try {
          return JsonUtility.FromJson<PhotonMenuSceneInfo>(PlayerPrefs.GetString("Photon.Menu.Scene"));
        }
        catch {
          return default(PhotonMenuSceneInfo);
        }
      }
      set => PlayerPrefs.SetString("Photon.Menu.Scene", JsonUtility.ToJson(value));
    }

    /// <summary>
    /// Toggle to create or join-only game sessions/rooms.
    /// </summary>
    public virtual bool Creating { get; set; }

    /// <summary>
    /// Partial method to expand defaults to SDK variations.
    /// </summary>
    /// <param name="config"></param>
    partial void SetDefaultsUser(FusionMenuConfig config);

    /// <summary>
    /// Make sure that all configuration have a default settings.
    /// </summary>
    /// <param name="config">The menu config.</param>
    public virtual void SetDefaults(FusionMenuConfig config) {
      Session = null;
      Creating = false;

      if (AppVersion == null || (AppVersion != config.MachineId && config.AvailableAppVersions.Contains(AppVersion) == false)) {
        AppVersion = config.MachineId;
      }

      if (PreferredRegion != null && config.AvailableRegions.Contains(PreferredRegion) == false) {
        PreferredRegion = string.Empty;
      }

      if (MaxPlayerCount <= 0 || MaxPlayerCount > config.MaxPlayerCount) {
        MaxPlayerCount = config.MaxPlayerCount;
      }

      if (string.IsNullOrEmpty(Username)) {
        Username = $"Player{config.CodeGenerator.Create(3)}";
      }

      if (config.AvailableScenes.Count > 0) {
        var index = config.AvailableScenes.FindIndex(s => s.Name == Scene.Name && s.ScenePath == Scene.ScenePath);
        if (index >= 0) {
          // Overwrite anything in storage with fresh information from the config
          Scene = config.AvailableScenes[Mathf.Clamp(index, 0, config.AvailableScenes.Count - 1)];
        } else {
          // Always set a valid scene if any are available.
          Scene = config.AvailableScenes[0];
        }
      }

      SetDefaultsUser(config);
    }
  }
}

#endregion


#region FusionMenuConnectFailReason.cs

namespace Fusion.Menu {
  /// <summary>
  /// Is used to convey some information about a connection error back to the caller.
  /// Is not an enum to allow SDK implementation to add errors.
  /// </summary>
  public partial class ConnectFailReason {
    /// <summary>
    /// No reason code available.
    /// </summary>
    public const int None = 0;
    /// <summary>
    /// User requested cancellation or disconnect.
    /// </summary>
    public const int UserRequest = 1;
    /// <summary>
    /// App or Editor closed
    /// </summary>
    public const int ApplicationQuit = 2;
    /// <summary>
    /// Connection disconnected.
    /// </summary>
    public const int Disconnect = 3;
  }
}

#endregion


#region FusionMenuConnectionBehaviour.cs

namespace Fusion.Menu {
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using UnityEngine.Events;

  /// <summary>
  /// 
  /// </summary>
  public abstract partial class FusionMenuConnectionBehaviour : FusionMonoBehaviour {
    
    /// <summary>
    /// Access the session name/ Photon room name.
    /// </summary>
    public abstract string SessionName { get; }
    /// <summary>
    /// Access the max player count.
    /// </summary>
    public abstract int MaxPlayerCount { get; }
    /// <summary>
    /// Access the actual region connected to.
    /// </summary>
    public abstract string Region { get; }
    /// <summary>
    /// Access the AppVersion used.
    /// </summary>
    public abstract string AppVersion { get; }
    /// <summary>
    /// Get a list of usernames that are inside this session.
    /// </summary>
    public abstract List<string> Usernames { get; }
    /// <summary>
    /// Is connection alive.
    /// </summary>
    public abstract bool IsConnected { get; }
    /// <summary>
    /// Get current connection ping.
    /// </summary>
    public abstract int Ping { get; }
    
    
    /// <summary>
    /// A shortcut to inject any code to change the connection args before the connection is started.
    /// </summary>
    public UnityEvent<FusionMenuConnectArgs> OnBeforeConnect;
    /// <summary>
    /// A shortcut to easily get notified about an impending disconnection.
    /// </summary>
    public UnityEvent<int> OnBeforeDisconnect;
    /// <summary>
    /// 
    /// </summary>
    public UnityEvent<string> OnProgress;

    /// <summary>
    /// Connect using <see cref="IFusionMenuConnectArgs"/>.
    /// </summary>
    /// <param name="connectionArgs">Connection arguments.</param>
    /// <returns>When the connection is established</returns>
    public virtual async Task<ConnectResult> ConnectAsync(FusionMenuConnectArgs connectionArgs) {
      if (OnBeforeConnect != null) {
        try {
          OnBeforeConnect.Invoke(connectionArgs);
        }
        catch (Exception e) {
          UnityEngine.Debug.LogException(e);
          return new ConnectResult() {
            FailReason = ConnectFailReason.Disconnect, DebugMessage = e.Message
          };
        }
      }

      return await ConnectAsyncInternal(connectionArgs);
    }

    /// <summary>
    /// Disconnect the current connection.
    /// </summary>
    /// <param name="reason">The disconnect reason <see cref="ConnectFailReason"/></param>
    /// <returns></returns>
    public virtual async Task DisconnectAsync(int reason) {
      if (OnBeforeDisconnect != null) {
        try {
          OnBeforeDisconnect.Invoke(reason);
        } catch (Exception e) {
          UnityEngine.Debug.LogException(e);
        }
      }

      await DisconnectAsyncInternal(reason);
    }

    /// <summary>
    /// Requests a list of available regions from the name server.
    /// </summary>
    /// <param name="connectionArgs">Connection arguments</param>
    /// <returns>List of available region configured in the dashboard for this app.</returns>
    public abstract Task<List<FusionMenuOnlineRegion>> RequestAvailableOnlineRegionsAsync(FusionMenuConnectArgs connectionArgs);
    
    /// <summary>
    /// The connection task.
    /// </summary>
    /// <param name="connectArgs">Connection args.</param>
    /// <returns>When the connection is established and the game ready.</returns>
    protected abstract Task<ConnectResult> ConnectAsyncInternal(FusionMenuConnectArgs connectArgs);
    /// <summary>
    /// Disconnect task.
    /// </summary>
    /// <param name="reason">Disconnect reason <see cref="ConnectFailReason>"/></param>
    /// <returns>When the connection has terminated gracefully.</returns>
    protected abstract Task DisconnectAsyncInternal(int reason);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    protected void ReportProgress(string message) {
      if (OnProgress != null) {
        OnProgress.Invoke(message);
      }
    }

  }
}

#endregion


#region FusionMenuConnectResult.cs

namespace Fusion.Menu {
  using System.Threading.Tasks;

  /// <summary>
  /// Connection result info object.
  /// </summary>
  public partial class ConnectResult {
    /// <summary>
    /// Is successful
    /// </summary>
    public bool Success;
    /// <summary>
    /// The fail reason code <see cref="ConnectFailReason"/>
    /// </summary>
    public int FailReason;
    /// <summary>
    /// Another custom code that can be filled by out by RealtimeClient.DisconnectCause for example.
    /// </summary>
    public int DisconnectCause;
    /// <summary>
    /// A debug message.
    /// </summary>
    public string DebugMessage;
    /// <summary>
    /// Set to true to disable all error handling by the menu.
    /// </summary>
    public bool CustomResultHandling;
    /// <summary>
    /// An optional task to signal the menu to wait until cleanup operation have completed (e.g. level unloading).
    /// </summary>
    public Task WaitForCleanup;
    
    public static ConnectResult Ok() {
      return new ConnectResult { Success = true };
    }
    
    public static ConnectResult Fail(int failReason, string debugMessage = null, Task waitForCleanup = null) {
      return new ConnectResult {
        Success = false,
        FailReason = failReason,
        DebugMessage = debugMessage,
        WaitForCleanup = waitForCleanup
      };
    }
  }
}

#endregion


#region FusionMenuGraphicsSettings.cs

namespace Fusion.Menu {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;

  /// <summary>
  /// Graphics settings that can be changed in the settings screen.
  /// Selected values are stored in <see cref="PlayerPrefs"/>
  /// Use <see cref="Apply()"/> to apply all after starting the app.
  /// </summary>
  public partial class FusionMenuGraphicsSettings {
    /// <summary>
    /// Available framerates.
    /// -1 = platform default
    /// </summary>
    protected static int[] PossibleFramerates = new int[] { -1, 30, 60, 75, 90, 120, 144, 165, 240, 360 };

    /// <summary>
    /// Target framerate
    /// </summary>
    public virtual int Framerate {
      get {
        var f = PlayerPrefs.GetInt("Photon.Menu.Framerate", -1);
        if (PossibleFramerates.Contains(f) == false) {
          return PossibleFramerates[0];
        }
        return f;
      }
      set => PlayerPrefs.SetInt("Photon.Menu.Framerate", value);
    }

    /// <summary>
    /// Fullscreen mode.
    /// Is not shown for mobile platforms.
    /// </summary>
    public virtual bool Fullscreen {
      get => PlayerPrefs.GetInt("Photon.Menu.Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
      set => PlayerPrefs.SetInt("Photon.Menu.Fullscreen", value ? 1 : 0);
    }

    /// <summary>
    /// Selected resolution index based on Screen.resolutions.
    /// Is not shown for mobile platforms.
    /// </summary>
    public virtual int Resolution {
      get => Math.Clamp(PlayerPrefs.GetInt("Photon.Menu.Resolution", GetCurrentResolutionIndex()), 0, Math.Max(0, Screen.resolutions.Length - 1));
      set => PlayerPrefs.SetInt("Photon.Menu.Resolution", value);
    }

    /// <summary>
    /// Select VSync.
    /// </summary>
    public virtual bool VSync {
      get => PlayerPrefs.GetInt("Photon.Menu.VSync", Math.Clamp(QualitySettings.vSyncCount, 0, 1)) == 1;
      set => PlayerPrefs.SetInt("Photon.Menu.VSync", value ? 1 : 0);
    }

    /// <summary>
    /// Select Unity quality level index based on QualitySettings.names.
    /// </summary>
    public virtual int QualityLevel {
      get {
        var q = PlayerPrefs.GetInt("Photon.Menu.QualityLevel", QualitySettings.GetQualityLevel());
        q = Math.Clamp(q, 0, QualitySettings.names.Length - 1);
        return q;
      }
      set => PlayerPrefs.SetInt("Photon.Menu.QualityLevel", value);
    }

    /// <summary>
    /// Return a list of possible framerates filtered by Screen.currentResolution.refreshRate.
    /// </summary>
    public virtual List<int> CreateFramerateOptions => PossibleFramerates.Where(f => f <=
#if UNITY_2022_2_OR_NEWER
      (int)Math.Round(Screen.currentResolution.refreshRateRatio.value)
#else
  Screen.currentResolution.refreshRate
#endif
      ).ToList();

    /// <summary>
    /// Returns a list of resolution option indices based on Screen.resolutions.
    /// </summary>
    public virtual List<int> CreateResolutionOptions => Enumerable.Range(0, Screen.resolutions.Length).ToList();

    /// <summary>
    /// Returns a list of graphics quality indices based on QualitySettings.names.
    /// </summary>
    public virtual List<int> CreateGraphicsQualityOptions => Enumerable.Range(0, QualitySettings.names.Length).ToList();

    /// <summary>
    /// A partial method to be implemented on the SDK level.
    /// </summary>
    partial void ApplyUser();

    /// <summary>
    /// Applies all graphics settings.
    /// </summary>
    public virtual void Apply() {
#if !UNITY_IOS && !UNITY_ANDROID
      if (Screen.resolutions.Length > 0) {
        var resolution = Screen.resolutions[Resolution < 0 ? Screen.resolutions.Length - 1 : Resolution];
        if (Screen.currentResolution.width != resolution.width || 
          Screen.currentResolution.height != resolution.height ||
          Screen.fullScreen != Fullscreen) { 
          Screen.SetResolution(resolution.width, resolution.height, Fullscreen);
        }
      }
#endif

      if (QualitySettings.GetQualityLevel() != QualityLevel) {
        QualitySettings.SetQualityLevel(QualityLevel);
      }

      if (QualitySettings.vSyncCount != (VSync ? 1 : 0)) {
        QualitySettings.vSyncCount = VSync ? 1 : 0;
      }

      if (Application.targetFrameRate != Framerate) {
        Application.targetFrameRate = Framerate;
      }

      ApplyUser();
    }

    /// <summary>
    /// Return the current selected resolution index based on Screen.resolutions.
    /// </summary>
    /// <returns>Index into Screen.resolutions</returns>
    private int GetCurrentResolutionIndex() {
      var resolutions = Screen.resolutions;
      if (resolutions == null || resolutions.Length == 0)
        return -1;

      int currentWidth = Mathf.RoundToInt(Screen.width);
      int currentHeight = Mathf.RoundToInt(Screen.height);
#if UNITY_2022_2_OR_NEWER
      var defaultRefreshRate = resolutions[^1].refreshRateRatio;
#else
      var defaultRefreshRate = resolutions[^1].refreshRate;
#endif

      for (int i = 0; i < resolutions.Length; i++) {
        var resolution = resolutions[i];

        if (resolution.width == currentWidth
          && resolution.height == currentHeight
#if UNITY_2022_2_OR_NEWER
          && resolution.refreshRateRatio.denominator == defaultRefreshRate.denominator
          && resolution.refreshRateRatio.numerator == defaultRefreshRate.numerator)
#else
          && resolution.refreshRate == defaultRefreshRate)
#endif
          return i;
      }

      return -1;
    }
  }
}

#endregion


#region FusionMenuMppmJoinCommand.cs

namespace Fusion.Menu {
  using System;
  using System.Threading.Tasks;
  using UnityEngine;
  using static UnityEngine.Object;
  using static FusionUnityExtensions;

  [Serializable]
  public partial class FusionMenuMppmJoinCommand : FusionMppmCommand {
    public string AppVersion;
    public string Region;
    public string Session;
      
    public override void Execute() {
      var task = ExecuteAsync();
      // trace errors
      task.ContinueWith(t => Debug.LogError(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
    }

    private async Task ExecuteAsync() {
      var controller = FindAnyObjectByType<FusionMenuUIController>();
      if (!controller) {
        return;
      }
      
      Assert.Check(controller.ConnectArgs != null);
      Apply(controller.ConnectArgs);
      
      controller.Show<FusionMenuUILoading>();
      
      var connectionResult = await controller.Connection.ConnectAsync(controller.ConnectArgs);
      await controller.HandleConnectionResult(connectionResult, controller);
    }
    
    public void Apply(FusionMenuConnectArgs args) {
      args.AppVersion = AppVersion;
      args.Region = Region;
      args.Session = Session;
      args.Creating = false;
      ApplyUser(args);
    }
    
    partial void ApplyUser(FusionMenuConnectArgs args);
  }
}

#endregion


#region FusionMenuOnlineRegion.cs

namespace Fusion.Menu {
  using System;

  /// <summary>
  /// Includes Photon ping regions result used by the Party menu to pre select the best region and encode the region into the party code.
  /// </summary>
  [Serializable]
  public struct FusionMenuOnlineRegion {
    /// <summary>
    /// Photon region code.
    /// </summary>
    public string Code;
    /// <summary>
    /// Last ping result.
    /// </summary>
    public int Ping;
  }
}

#endregion


#region FusionMenuSceneInfo.cs

namespace Fusion.Menu {
  using System;
  using System.IO;
  using UnityEngine;

  /// <summary>
  /// Info struct for creating configurable selectable scenes in the Photon menu.
  /// </summary>
  [Serializable]
  public partial struct PhotonMenuSceneInfo {
    /// <summary>
    /// Displayed scene name.
    /// </summary>
    public string Name;
    /// <summary>
    /// The path to the scene asset.
    /// </summary>
    [ScenePath] public string ScenePath;
    /// <summary>
    /// Gets the filename of the ScenePath to set as Unity scene to load during connection sequence.
    /// </summary>
    public string SceneName => ScenePath == null ? null : Path.GetFileNameWithoutExtension(ScenePath);
    /// <summary>
    /// The sprite displayed as scene preview in the scene selection UI.
    /// </summary>
    public Sprite Preview;
  }
}

#endregion


#region FusionMenuSettingsEntry.cs

namespace Fusion.Menu {
  using System;
  using System.Collections.Generic;
  using System.Linq;
#if FUSION_ENABLE_TEXTMESHPRO
  using Dropdown = TMPro.TMP_Dropdown;
#else 
  using Dropdown = UnityEngine.UI.Dropdown;
#endif

  /// <summary>
  /// A helper class that maps a option name into the actual value.
  /// Is used to simplifiy dropdown UI code in <see cref="FusionMenuUISettings"/>.
  /// </summary>
  /// <typeparam name="T">The option value type</typeparam>
  public class FusionMenuSettingsEntry<T> where T : IEquatable<T> {
    private Dropdown _dropdown;
    private List<T> _options;

    /// <summary>
    /// Returns the value of this option.
    /// </summary>
    public T Value => _options == null || _options.Count == 0 ? default(T) : _options[_dropdown.value];

    /// <summary>
    /// Creates an option for this dropdown element.
    /// </summary>
    /// <param name="dropdown">Dropdown UI element</param>
    /// <param name="onValueChanged">Forward the value chaged callback</param>
    public FusionMenuSettingsEntry(Dropdown dropdown, Action onValueChanged) {
      _dropdown = dropdown;
      _dropdown.onValueChanged.RemoveAllListeners();
      _dropdown.onValueChanged.AddListener(_ => onValueChanged.Invoke());
    }

    /// <summary>
    /// Clear all options and set new.
    /// </summary>
    /// <param name="options">List of options</param>
    /// <param name="current">The current selected option</param>
    /// <param name="ToString">A callback to format the option text</param>
    public void SetOptions(List<T> options, T current, Func<T, string> ToString = null) {
      _options = options;
      _dropdown.ClearOptions();
      _dropdown.AddOptions(options.Select(o => ToString != null ? ToString(o) : o.ToString()).ToList());

      var index = _options.FindIndex(0, o => o.Equals(current));
      if (index >= 0) {
        _dropdown.SetValueWithoutNotify(index);
      } else {
        _dropdown.SetValueWithoutNotify(0);
      }
    }
  }
}

#endregion


#region FusionMenuUIScreen.cs

namespace Fusion.Menu {
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  /// <summary>
  /// The screen base class contains a lot of accessors (e.g. Config, Connection, ConnectArgs) for convenient access.
  /// </summary>
  public abstract class FusionMenuUIScreen : FusionMonoBehaviour {
    /// <summary>
    /// Cached Hide animation hash.
    /// </summary>
    protected static readonly int HideAnimHash = Animator.StringToHash("Hide");
    /// <summary>
    /// Cached Show animation hash.
    /// </summary>
    protected static readonly int ShowAnimHash = Animator.StringToHash("Show");

    /// <summary>
    /// Is modal flag must be set for overlay screens.
    /// </summary>
    [InlineHelp, SerializeField] private bool _isModal;
    /// <summary>
    /// The list of screen plugins for the screen. The actual plugin scripts can be distributed insde the UI hierarchy but must be liked here.
    /// </summary>
    [InlineHelp, SerializeField] private List<FusionMenuScreenPlugin> _plugins;
    /// <summary>
    /// The animator object.
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// The hide animation coroutine.
    /// </summary>
    private Coroutine _hideCoroutine;

    /// <summary>
    /// The list of screen plugins.
    /// </summary>
    public List<FusionMenuScreenPlugin> Plugins => _plugins;
    /// <summary>
    /// Is modal property.
    /// </summary>
    public bool IsModal => _isModal;
    /// <summary>
    /// Is the screen currently showing.
    /// </summary>
    public bool IsShowing { get; private set; }
    /// <summary>
    /// The menu config, assigned by the <see cref="IFusionMenuUIController"/>.
    /// </summary>
    public FusionMenuConfig Config { get; set; }
    /// <summary>
    /// The menu connection object, The menu config, assigned by the <see cref="IFusionMenuUIController"/>.
    /// </summary>
    public FusionMenuConnectionBehaviour Connection { get; set; }
    /// <summary>
    /// The menu connection args.
    /// </summary>
    public FusionMenuConnectArgs ConnectionArgs { get; set; }
    /// <summary>
    /// The menu UI controller that owns this screen.
    /// </summary>
    public FusionMenuUIController Controller { get; set; }

    /// <summary>
    /// Unity start method to find the animator.
    /// </summary>
    public virtual void Start() {
      TryGetComponent(out _animator);
    }

    /// <summary>
    /// Unit awake method to be overwritten by derived screens.
    /// </summary>
    public virtual void Awake() {
    }

    /// <summary>
    /// The screen init method is called during <see cref="FusionMenuUIController{T}.Awake()"/> after all screen have been assigned and configured.
    /// </summary>
    public virtual void Init() {
    }

    /// <summary>
    /// The screen hide method.
    /// </summary>
    public virtual void Hide() {
      if (_animator) {
        if (_hideCoroutine != null) {
          StopCoroutine(_hideCoroutine);
        }

        _hideCoroutine = StartCoroutine(HideAnimCoroutine());
        return;
      }

      IsShowing = false;

      foreach (var p in _plugins) {
        p.Hide(this);
      }

      gameObject.SetActive(false);
    }

    /// <summary>
    /// The screen show method.
    /// </summary>
    public virtual void Show() {
      if (_hideCoroutine != null) {
        StopCoroutine(_hideCoroutine);
        if (_animator.gameObject.activeInHierarchy && _animator.HasState(0, ShowAnimHash)) {
          _animator.Play(ShowAnimHash, 0, 0);
        }
      }

      gameObject.SetActive(true);

      IsShowing = true;

      foreach (var p in _plugins) {
        p.Show(this);
      }
    }

    /// <summary>
    /// Play the hide animation wrapped in a coroutine.
    /// Forces the target framerate to 60 during the transition animations.
    /// </summary>
    /// <returns>When done</returns>
    private IEnumerator HideAnimCoroutine() {
#if UNITY_IOS || UNITY_ANDROID
      var changedFramerate = false;
      if (Config.AdaptFramerateForMobilePlatform) {
        if (Application.targetFrameRate < 60) {
          Application.targetFrameRate = 60;
          changedFramerate = true;
        }
      }
#endif
      
      _animator.Play(HideAnimHash);
      yield return null;
      while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
        yield return null;
      }

#if UNITY_IOS || UNITY_ANDROID
      if (changedFramerate) {
        new FusionMenuGraphicsSettings().Apply();
      }
#endif

      gameObject.SetActive(false);
    }
  }
}

#endregion

