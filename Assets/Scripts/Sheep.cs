using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float speed;
    public float walkSpeed;
    public float maxDist_P;
    public float maxDist_RP;
    public float miniDist_R;

    private Transform reader;
    private Transform player;
    private Rigidbody2D rb;

    private void Start() {
        reader = GameObject.Find("Reader").transform;
        player = GameObject.Find("Player").transform;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(name == "Reader") {
            if(Vector2.Distance(transform.position, player.position) < maxDist_RP) {
                Movement(player, false);
                speed = player.GetComponent<Player>().speed - 1.5f;
            } else {
                Movement(player, true, false);
            }
        } else {
            if(Vector2.Distance(transform.position, player.position) < maxDist_P) {
                Movement(player, false);
            } else if(Vector2.Distance(transform.position, reader.position) > miniDist_R) {
                Movement(reader, true);
            }
            speed = reader.GetComponent<Sheep>().speed;
        }
    }

    private void Movement(Transform target, bool front, bool movement = true) {
        Vector2 dirVec;
        if(front) dirVec = target.position - transform.position;
        else dirVec = transform.position - target.position;

        Vector2 nextVec = dirVec.normalized * walkSpeed * Time.fixedDeltaTime;

        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        rb.SetRotation(Quaternion.AngleAxis(angle - 90, Vector3.forward));

        if(movement) {
            rb.MovePosition(rb.position + nextVec);
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Finish")) {
            SheepManager.Finish();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Fence") && name == "Reader") {
            SheepManager.Fail();
        }
    }
}