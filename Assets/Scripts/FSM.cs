using UnityEngine;

public class FSM : MonoBehaviour
{
	public Transform player;
	public float patrolSpeed = 2f;
	public float chaseSpeed = 4f;
	public float patrolRadius = 5f;
	public float chaseDistance = 10f;
	public float attackDistance = 2f;

	private Vector3 patrolPoint;
	private enum State { Patrol, Chase, Attack }
	private State currentState;

	void Start()
	{
		currentState = State.Patrol;
		SetNewPatrolPoint();
	}

	void Update()
	{
		float distance = Vector3.Distance(transform.position, player.position);

		// FSM transitions
		if (distance > chaseDistance)
			currentState = State.Patrol;
		else if (distance > attackDistance)
			currentState = State.Chase;
		else
			currentState = State.Attack;

		switch (currentState)
		{
			case State.Patrol:
				Patrol();
				break;
			case State.Chase:
				Chase();
				break;
			case State.Attack:
				Attack();
				break;
		}
	}

	void Patrol()
	{
		transform.position = Vector3.MoveTowards(transform.position, patrolPoint, patrolSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, patrolPoint) < 0.5f)
			SetNewPatrolPoint();
	}

	void SetNewPatrolPoint()
	{
		Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
		patrolPoint = new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);
	}

	void Chase()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
	}

	void Attack()
	{
		// Здесь можно добавить анимацию атаки или логику урона
		Debug.Log("Attack!");
	}
}