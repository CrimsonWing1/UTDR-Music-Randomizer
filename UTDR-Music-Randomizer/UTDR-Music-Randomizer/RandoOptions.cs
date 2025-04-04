using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDRMusicRandomizer
{
    public class RandoOptions
    {
        public bool Intros { get; }
        public bool CreditsSongs { get; }
        public bool Ambience { get; }
        public bool SFX { get; }

        public RandoOptions(bool intro,
            bool creditsSongs, bool ambience, bool sfx)
        {
            Intros = intro;
            CreditsSongs = creditsSongs;
            Ambience = ambience;
            SFX = sfx;
        }
    }
}
