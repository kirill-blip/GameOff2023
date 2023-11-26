using Stride.Audio;
using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOff2023
{
    public class AudioManager : AsyncScript
    {
        public List<Sound> Sounds = new List<Sound>();
        public float Volume = 0.5f;

        private SoundInstance _musicInstance;

        public override async Task Execute()
        {
            Random random = new Random();
            _musicInstance = Sounds[random.Next(0, Sounds.Count)].CreateInstance();

            await _musicInstance.ReadyToPlay();

            _musicInstance.Play();

            _musicInstance.Volume = Volume;

            while (Game.IsRunning)
            {
                if (_musicInstance.PlayState == Stride.Media.PlayState.Stopped)
                {
                    Sound sound = Sounds[random.Next(0, Sounds.Count)];
                    
                    _musicInstance = sound.CreateInstance();
                    _musicInstance.Volume = Volume;
                    
                    await _musicInstance.ReadyToPlay();

                    _musicInstance.Play();
                }

                await Script.NextFrame();
            }
        }
    }
}
