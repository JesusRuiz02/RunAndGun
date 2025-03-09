using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI User;
  [SerializeField] private TextMeshProUGUI Value;

  public void ChangeUser(string user)
  {
    User.text = user;
  }
  public void ChangeValue(string value)
  {
    Value.text = value;
  }
}
