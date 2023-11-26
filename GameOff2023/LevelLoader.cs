using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Core.Serialization;

namespace GameOff2023
{
    public class LevelLoader : StartupScript
    {
        public UrlReference<Scene> MenuScene;
        public UrlReference<Scene> LevelScene;

        public override void Start() { }

        public void LoadLevel()
        {
            LoadScene(LevelScene);
        }

        public void LoadMenu()
        {
            LoadScene(MenuScene);
        }

        public void LoadScene(UrlReference<Scene> scene)
        {
            Content.Unload(SceneSystem.SceneInstance.RootScene);
            SceneSystem.SceneInstance.RootScene = Content.Load(scene);
        }
    }
}
