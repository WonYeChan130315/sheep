using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float runMana;
    public float walkSpeed;

    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float runSpeed;
    private float slowSpeed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        runSpeed = walkSpeed + 2;
        slowSpeed = walkSpeed - 2;
    }

    private void Update() {
        if(runMana < 2 && !Input.GetKey(KeyCode.LeftShift)) {
            runMana += Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftShift) && runMana > 0) {
            speed = runSpeed;
            runMana -= Time.deltaTime;
        } else if(Input.GetKey(KeyCode.LeftControl)) {
            speed = slowSpeed;
        } else {
            speed = walkSpeed;
        }
    }

    private void FixedUpdate() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(0) && Vector2.Distance(transform.position, mousePos) > 0.6f) {
            Vector2 dirVec = mousePos - transform.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

            float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
            rb.SetRotation(Quaternion.AngleAxis(angle - 90, Vector3.forward));

            rb.MovePosition(rb.position + nextVec);
            rb.velocity = Vector2.zero;
        }
    }
}