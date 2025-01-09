using System;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IPlayerState
{
    void EnterState(PlayerMovement player); //  이 상태가 되었을 때 시행할 것
    void UpdateState(PlayerMovement player);
    void FixedUpdateState(PlayerMovement player);
}

public class IdleState : IPlayerState
{
    public void EnterState(PlayerMovement player)
    {
        player.animator.SetBool("IsWalk", false);
    }

    public void UpdateState(PlayerMovement player)
    {
        float horizontalInput = player.joyStick.joyStickX;

        if (horizontalInput != 0)
        {
            // 플레이어 에게 상태 변화 지시
            player.ChangeState(new WalkingState());
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump())
        {
            // 플레이어 에게 상태 변화 지시
            player.ChangeState(new JumpingState());            
        }

        if (Input.GetKeyDown(KeyCode.L) && player.CanShoot())
        {
            // 플레이어 에게 상태 변화 지시
            player.ChangeState(new ShootingState());            
        }
    }

    public void FixedUpdateState(PlayerMovement player)
    {
    }


    public class WalkingState : IPlayerState
    {
        public void EnterState(PlayerMovement player)
        {
            player.animator.SetBool("IsWalk", true);
        }

        public void UpdateState(PlayerMovement player)
        {
            float horizontalInput = player.joyStick.joyStickX;

            if (horizontalInput < 0)
            {
                player.FacingRight = -1;
                // 쿼터니언 오일러로 사용할수도 있음 (3개 변수)
                // player.transform.rotation = Quaternion.Euler();
                player.transform.rotation = new Quaternion(0, 180, 0, 0);
            } else if (horizontalInput > 0)
            {
                player.FacingRight = 1;
                player.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (horizontalInput == 0)
            {
                player.ChangeState(new IdleState());
                
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && player.CanJump())
            {
                // 플레이어 에게 상태 변화 지시
                player.ChangeState(new JumpingState());            
            }

            if (Input.GetKeyDown(KeyCode.L) && player.CanShoot())
            {
                // 플레이어 에게 상태 변화 지시
                player.ChangeState(new ShootingState());            
            }
        }

        public void FixedUpdateState(PlayerMovement player)
        {
            float horizontalInput = player.joyStick.joyStickX;
            player.MyRigid.velocity = new Vector2(horizontalInput * player.movePower, player.MyRigid.velocity.y);
        }
    }

    public class JumpingState : IPlayerState
    {
        public void EnterState(PlayerMovement player)
        {
            player.MyRigid.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
            player.JumpTime++;
        }

        public void UpdateState(PlayerMovement player)
        {
            if (player.MyRigid.velocity.y <= 0 && player.JumpTime == 0)
            {
                player.ChangeState(new IdleState());
            }
        }

        public void FixedUpdateState(PlayerMovement player)
        {

        }
    }
    
    public class ShootingState : IPlayerState
    {
        public void EnterState(PlayerMovement player)
        {
            GameObject firedBullet = Object.Instantiate(player.playerBullet, player.playerMuzzle.transform.position, player.transform.rotation);
            firedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * player.FacingRight, 0);
            player.ResetShootTimer();
            
            player.animator.SetTrigger("IsShoot");
            player.ChangeState(new IdleState());
        }

        public void UpdateState(PlayerMovement player)
        {
    
        }

        public void FixedUpdateState(PlayerMovement player)
        {
            throw new NotImplementedException();
        }
    }
}
