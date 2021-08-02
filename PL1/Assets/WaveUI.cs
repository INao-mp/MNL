using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("no spawner referenced!");
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            Debug.LogError("no waveAnimator referenced!");
            this.enabled = false;
        }

        if (waveCountdownText == null)
        {
            Debug.LogError("no waveCountdownText referenced!");
            this.enabled = false;
        }

        if (waveCountText == null)
        {
            Debug.LogError("no waveCountText referenced!");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
            //case WaveSpawner.SpawnState.WAITING:
              //  break;
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
          //  default:
            //    break;
        }

        previousState = spawner.State;
    }

    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountfown", true);
            Debug.Log("Counting");
        }

        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountfown", false);
            waveAnimator.SetBool("WaveIncoming", true);
            Debug.Log("Spawning");
        }

        waveCountText.text = ((int)spawner.NextWave).ToString();
        
    }
}
