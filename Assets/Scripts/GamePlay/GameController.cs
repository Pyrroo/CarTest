using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasSwaper canvasSwaper;
    [SerializeField] private GamePlayStartTimer gamePlayStartTimer;
    [SerializeField] private TextMeshProUGUI raceInfo;

    [Header("Prefabs & Spawn")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform spawnPoint;

    public static bool isMenu = true;

    private GhostRecorder ghostRecorder;
    private GhostPlayer ghostPlayer;

    private bool raceWithGhost;
    private bool raceWithRecord;

    private void Awake()
    {
        gamePlayStartTimer.RaceStarted += OnRaceStart;
    }

    private void OnDestroy()
    {
        gamePlayStartTimer.RaceStarted -= OnRaceStart;
    }

    #region ��������� ������ �����
    public void StartGameWithRecord()
    {
        raceWithGhost = false;
        raceWithRecord = true;

        raceInfo.text = "Solo Race";
        PrepareRace();

        var player = Spawn(playerPrefab);
        ghostRecorder = player.GetComponent<GhostRecorder>();

        Finish.Finished += StopRace;
    }

    public void StartGameWithGhost()
    {
        raceWithGhost = true;
        raceWithRecord = false;

        raceInfo.text = "Race with Ghost";
        PrepareRace();

        var ghost = Spawn(ghostPrefab);
        ghostPlayer = ghost.GetComponent<GhostPlayer>();
        ghostPlayer.sourceRecorder = ghostRecorder;

        Destroy(ghostRecorder.gameObject); // ������� ������� ������-������

        Spawn(playerPrefab);

        Finish.Finished += StopRace;
    }
    #endregion

    #region ����� ������ ������/�����
    private void PrepareRace()
    {
        isMenu = true;
        canvasSwaper.ToGame();
        gamePlayStartTimer.StartTimer();
    }

    private void StopRace()
    {
        Finish.Finished -= StopRace;

        if (raceWithRecord && ghostRecorder != null)
            ghostRecorder.StopRecording();

        if (raceWithGhost)
        {
            // ������������� ����� ����� ����� � ���������
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        canvasSwaper.ToEnd();
    }
    #endregion

    #region ��������� ������� ������ �����
    private void OnRaceStart()
    {
        isMenu = false;

        if (raceWithRecord && ghostRecorder != null)
            ghostRecorder.StartRecording();

        if (raceWithGhost && ghostPlayer != null)
            ghostPlayer.StartPlayback();
    }
    #endregion

    #region �������
    private GameObject Spawn(GameObject prefab)
    {
        return Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
    #endregion
}
