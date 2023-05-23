using System.Collections;
using Mirror;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class JoinTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator JoinHost()
        {
            yield return LoadMenu();
            NetworkManager network = GameObject.FindObjectOfType<NetworkManager>();
            network.StartHost();
            yield return new WaitForSeconds(2);
            Assert.IsTrue(network.isNetworkActive);
            network.StopHost();
        }

        private IEnumerator LoadMenu()
        {
            SceneManager.LoadScene("Menu");
            yield return new WaitForSeconds(1);
        }
        
        /*[UnityTest]
        public IEnumerator JoinClient()
        {
            yield return LoadMenu();
            NetworkManager network = GameObject.FindObjectOfType<NetworkManager>();
            network.StartClient();
            yield return new WaitForSeconds(2);
            Assert.IsTrue(network.isNetworkActive);
            network.StopClient();
        }*/
    }
}
