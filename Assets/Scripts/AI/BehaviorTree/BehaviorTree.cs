using UnityEngine;
using System.Collections.Generic;

public class BehaviorTree 
{
    public enum Result { SUCCESS, FAILURE, IN_PROGRESS };

    public EnemyController agent;
    public Dictionary<string, object> blackboard;

    public virtual Result Run()
    {
        return Result.SUCCESS;
    }

    public BehaviorTree()
    {

    }

    public void SetAgent(EnemyController agent)
    {
        this.agent = agent;
    }

    #region ======== [ BLACKBOARD FUNCTIONS ] ========

    public void AddBlackboard(Dictionary<string, object> blackboard)
    {
        this.blackboard = blackboard;
    }


    /// <summary>
    /// Get the EnemyController value of the blackboard. 
    /// Returns null with a warning if it can't fetch the EnemyController value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected EnemyController GetBBEnemy(string key)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return null;
        }
        if (!blackboard.ContainsKey(key))
        {
            Debug.LogWarning($"" +
                $"Missing key '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster}.");
            return null;
        }
        if (!(blackboard[key] is EnemyController))
        {
            Debug.LogWarning($"" +
                $"Value for '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is not an EnemyController.");
            return null;
        }

        return (EnemyController)blackboard[key];
    }


    /// <summary>
    /// Get the string value of the blackboard. 
    /// Returns null with a warning if it can't fetch the string value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string GetBBString(string key)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return null;
        }
        if (!blackboard.ContainsKey(key))
        {
            Debug.LogWarning($"" +
                $"Missing key '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster}.");
            return null;
        }
        if (!(blackboard[key] is string))
        {
            Debug.LogWarning($"" +
                $"Value for '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is not a string.");
            return null;
        }

        return (string)blackboard[key];
    }


    /// <summary>
    /// Get the int value of the blackboard. 
    /// Returns 0 with a warning if it can't fetch the int value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected int GetBBInt(string key)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return 0;
        }
        if (!blackboard.ContainsKey(key))
        {
            Debug.LogWarning($"" +
                $"Missing key '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster}.");
            return 0;
        }
        if (!(blackboard[key] is int))
        {
            Debug.LogWarning($"" +
                $"Value for '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is not an int.");
            return 0;
        }

        return (int)blackboard[key];
    }


    /// <summary>
    /// Get the string value of the blackboard. 
    /// Returns null with a warning if it can't fetch the float value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected float GetBBFloat(string key)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return 0f;
        }
        if (!blackboard.ContainsKey(key))
        {
            Debug.LogWarning($"" +
                $"Missing key '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster}.");
            return 0f;
        }
        if (!(blackboard[key] is float))
        {
            Debug.LogWarning($"" +
                $"Value for '{key}' in blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is not a float.");
            return 0f;
        }

        return (float)blackboard[key];
    }


    /// <summary>
    /// Get the boolean value from the blackboard. 
    /// Returns false with a warning if it can't fetch the boolean value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected bool GetBBBool(string key)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"Blackboard for {agent.gameObject.name} of type {agent.monster} is null.");
            return false;
        }

        if (!blackboard.ContainsKey(key))
        {
            Debug.LogWarning($"Missing key '{key}' in blackboard for {agent.gameObject.name} of type {agent.monster}.");
            return false;
        }
        if (!(blackboard[key] is bool))
        {
            Debug.LogWarning($"Value for '{key}' in blackboard for {agent.gameObject.name} of type {agent.monster} is not a boolean.");
            return false;
        }
        
        return (bool)blackboard[key];
    }
   


    /// <summary>
    /// Adds a EnemyController entry to the blackboard
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    protected void SetBBEnemy(string key, EnemyController value)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return;
        }

        blackboard[key] = value;
    }


    /// <summary>
    /// Adds a string entry to the blackboard
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    protected void SetBBString(string key, string value)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return;
        }

        blackboard[key] = value;
    }


    /// <summary>
    /// Adds an int entry to the blackboard
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    protected void SetBBInt(string key, int value)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return;
        }

        blackboard[key] = value;
    }



    /// <summary>
    /// Adds a float entry to the blackboard
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    protected void SetBBFloat(string key, float value)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"" +
                $"Blackboard for {agent.gameObject.name} " +
                $"of type {agent.monster} is null.");
            return;
        }

        blackboard[key] = value;
    }

    /// <summary>
    /// Set the boolean value in the blackboard.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetBBBool(string key, bool value)
    {
        if (blackboard == null)
        {
            Debug.LogWarning($"Blackboard for {agent.gameObject.name} of type {agent.monster} is null.");
            return;
        }

        blackboard[key] = value;
    }

    #endregion

    public virtual IEnumerable<BehaviorTree> AllNodes()
    {
        yield return this;
    }

    public virtual BehaviorTree Copy()
    {
        return null;
    }
}
