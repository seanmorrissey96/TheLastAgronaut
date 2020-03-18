using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public BattleState state;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUDScript playerHUD;
    public BattleHUDScript enemyHUD;

    public GameObject levelChanger;
    public GameObject objectManager;

    public Text dialogueText;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "What will PENELOPE do?";
        Debug.Log("Enemy defense at start of turn: " + enemyUnit.defense);
    }

    public void onAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }
    public void onGlyphosateButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(GlyphosateAttack());
    }

    public void onTriazineButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(TriazineAttack());
    }

    public void onHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Penelope feels renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator GlyphosateAttack()
    {
        bool weakToAttack = false;
        System.Random rnd = new System.Random();

        //double damage = ((playerUnit.intelligence + playerUnit.attack1Strength) / enemyUnit.defense);
        int damage = (playerUnit.intelligence + playerUnit.attack1Strength) - (enemyUnit.defense * 2);
        foreach (string weakness in enemyUnit.weaknesses)
        {
            if (weakness == "Glyphosate")
            {
                damage = damage * 2;
                weakToAttack = true;
            }
        }

        Debug.Log("Damage before random: " + damage);
        //damage = damage * rnd.Next(1, 11);
        Debug.Log("Damage after random: " + damage);

        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Penelope sprays MUTATED PLANT with toxic GLYPHOSATE!";

        if (weakToAttack)
        {
            yield return new WaitForSeconds(2f);
            dialogueText.text = "MUTATED PLANT is weak to GLYPHOSATE!";
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator TriazineAttack()
    {
        bool weakToAttack = false;
        System.Random rnd = new System.Random();

        //double damage = ((playerUnit.intelligence + playerUnit.attack1Strength) / enemyUnit.defense);
        int damage = (playerUnit.intelligence + playerUnit.attack2Strength) - (enemyUnit.defense * 2);

        foreach (string weakness in enemyUnit.weaknesses)
        {
            if (weakness == "Triazine")
            {
                damage = damage * 2;
                weakToAttack = true;
            }
        }

        Debug.Log("Damage before random: " + damage);
        //damage = damage * rnd.Next(1, 11);
        Debug.Log("Damage after random: " + damage);

        bool isDead = enemyUnit.TakeDamage(damage);
        enemyUnit.lowerDefense(2);
        Debug.Log("Enemy defense after Triazine: " + enemyUnit.defense);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Penelope sprays MUTATED PLANT with toxic TRIAZINE!";

        if (weakToAttack)
        {
            yield return new WaitForSeconds(2f);
            dialogueText.text = "MUTATED PLANT is weak to TRIAZINE!";
        }

        yield return new WaitForSeconds(2f);

        dialogueText.text = "The TRIAZINE lowered its defense by 2!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Penelope stabs MUTATED PLANT with a pitchfork!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " spits an unknown substance at Penelope!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated!";
        }

        LevelChanger lc = levelChanger.GetComponent<LevelChanger>();
        
        lc.FadeToLevel(0);
    }
}
