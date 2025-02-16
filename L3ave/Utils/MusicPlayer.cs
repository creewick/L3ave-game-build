using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace L3ave.Utils
{
    internal class MusicPlayer
    {
        private ContentManager content;

        private static SoundEffect tick;

        private static SoundEffect light;

        private static SoundEffect shock;

        public MusicPlayer(ContentManager content)
        {
            this.content = content;

            tick = content.Load<SoundEffect>("tick");
            light = content.Load<SoundEffect>("light");
            shock = content.Load<SoundEffect>("shock");
        }

        public void PlayMusic(string name)
        {
            var song = content.Load<Song>(name);
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public void PlayEffect(string name)
        {
            var soundEffect = content.Load<SoundEffect>(name);
            soundEffect.Play();
        }

        public static void Light()
        {
            light.Play();
        }

        public static void Shock()
        {
            shock.Play();
        }

        public static void Tick()
        {
            tick.Play();
        }
    }
}