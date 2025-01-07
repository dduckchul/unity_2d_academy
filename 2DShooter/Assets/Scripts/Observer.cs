using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    void update(GameManager gameManager);
    void update(PlayerControler playerController);
}
