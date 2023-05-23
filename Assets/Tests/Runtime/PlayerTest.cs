using System.Collections;
using Mirror;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {
        private bool _isHosting;

        private IEnumerator LoadMenu()
        {
            SceneManager.LoadScene("Menu");
            yield return new WaitForSeconds(2);
        }
    
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return new EnterPlayMode();
            yield return LoadMenu();
            NetworkManager network = GameObject.FindObjectOfType<NetworkManager>();
            network.StartClient();
            yield return new WaitForSeconds(2);
            if (network.isNetworkActive)
            {
                _isHosting = false;
                yield break;
            }
            _isHosting = true;
            network.StartHost();
            yield return new WaitForSeconds(2);
        }
    
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            NetworkManager network = GameObject.FindObjectOfType<NetworkManager>();
            if (_isHosting)
            {
                network.StopHost();
            }
            else
            {
                network.StopClient();
            }
            yield return new ExitPlayMode();
        }
    
    
        [UnityTest]
        public IEnumerator PlayerSpawn()
        {
            PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();
            PlayerController localPlayer = null;
            foreach (var player in players)
            {
                if (player.isLocalPlayer)
                {
                    localPlayer = player;
                    break;
                }
            }
            Assert.IsNotNull(localPlayer);
            yield return null;
        }
    
        [UnityTest]
        public IEnumerator MovementTest()
        {
            PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();
            PlayerController localPlayer = null;
            foreach (var player in players)
            {
                if (player.isLocalPlayer)
                {
                    localPlayer = player;
                    break;
                }
            }
            Assert.IsNotNull(localPlayer);
            Vector3 originalPosition = localPlayer.transform.position;
            float t = 0;
            while (t< 2)
            {
                t += Time.deltaTime;
                localPlayer.OnMove(new Vector2(1, 0));
                yield return null;
            }
            Assert.AreNotEqual(originalPosition, localPlayer.transform.position);
            t = 0;
        }
    }
}
