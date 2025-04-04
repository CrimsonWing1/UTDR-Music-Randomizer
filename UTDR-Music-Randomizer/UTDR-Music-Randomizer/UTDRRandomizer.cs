using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDRMusicRandomizer
{
    public static class UTDRRandomizer
    {

        private static string[] CreditsSongs = new string[]
        {
            "credits", "a_call_to_action", "a_place_to_rest"
        };

        private static string[] Intros = new string[]
        {
            "mothers_love_phase_3_buildup", "mew_intro", "mothers_love_intro", "flowey_roof_intro_1", "flowey_roof_intro_2", "flowey_final_boss_1_intro", "dalvopening_yellow", "asgoreop"
        };

        private static string[] Ambience = new string[]
        {
            "starlo_entrance", "the_trek", "the_wandering", "train_trouble", "treading_lightly", "tucked_in", "uhoh", "well_be_okay", "nobodycame_yellow", "nothing_but_the_truth", "notsoquietstray", "ones_past", "quietstray", "sadlo", "sheriffs_fate", "shimmer", "shuffling1", "shuffling2", "shuffling3", "snow", "missing_inaction", "flowey_world", "gift_1", "gift_2", "detainment", "detour", "abandoned", "acquittal"
        };

        private static string[] SFX = new string[]
        {
            "UNDERTALE_oogloop", "wind", "snoring_justice", "honest_days_work", "intronoise", "mart_geno_wind_yellow", "mew_logo", "f_laugh", "f_newlaugh", "flowey_soundscape", "computer_ambience", "crescendo_of_dread", "ambient_river", "apex", "barrier", "birdnoise", "deal_em_out_ace_yellow", "deal_em_out_ed_yellow", "deal_em_out_mooch_yellow", "deal_em_out_moray_yellow", "honeydew_bark", "delivery_intro", "dual", "dual_short"
        };

        private struct SongFile
        {
            public SongFile(string name, bool fromBaseFolder = false)
            {
                Name = name;
                FromBaseFolder = fromBaseFolder;
            }
            public string Name { get; }
            public bool FromBaseFolder { get; }
        }

        public static bool IsValidPath(string path)
        {
            if (Directory.Exists(path))
            {
                if (File.Exists(path + @"\Undertale Yellow.exe") && Directory.Exists(path + @"\mus") && Directory.Exists(path + @"\snd"))
                {
                    return true;
                }
                else if (IsMac(path) || IsLinux(path))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private static char GetSeparator(string basePath)
        {
            if (IsMac(basePath) || IsLinux(basePath))
            {
                return '/';
            }
            else
            {
                return '\\';
            }
        }

        private static string GetBasePath(string installPath)
        {
            string basePath = installPath;
            if (IsMac(installPath))
            {
                basePath += "/Contents/Resources";
            }
            else if (IsLinux(installPath))
            {
                basePath += "/assets";
            }
            return basePath;
        }

        private static string GetMusicPath(string basePath)
        {
            string musicPath = GetBasePath(basePath);
            musicPath += GetSeparator(basePath) + "mus";
            return musicPath;
        }

        private static string GetSoundPath(string basePath)
        {
            string soundPath = GetBasePath(basePath);
            soundPath += GetSeparator(basePath) + "snd";
            return soundPath;
        }

        private static string GetBackupPath(string basePath)
        {
            return GetMusicPath(basePath) + GetSeparator(basePath) + "backup";
        }

        public static bool BackupFolderExists(string basePath)
        {
            return Directory.Exists(GetBackupPath(basePath));
        }

        public static RandoResult Randomize(string basePath, RandoOptions options, bool makeBackup)
        {
            string musicPath = GetMusicPath(basePath);
            string soundPath = GetSoundPath(basePath);
            string[] musicFiles = Directory.GetFiles(musicPath);
            string[] soundFiles = Directory.GetFiles(soundPath);
            IEnumerable<string> musicOGGs;
            
            musicOGGs =
                from f in files
                where Path.GetExtension(f) == ".ogg"
                where Path.GetFileNameWithoutExtension(f) != "null"
                where (options.Intros ? true : !Intros.Contains(Path.GetFileNameWithoutExtension(f).ToLower()))
                where (options.CreditsSongs ? true : !CreditsSongs.Contains(Path.GetFileNameWithoutExtension(f).ToLower()))
                where (options.Ambience ? true : !Ambience.Contains(Path.GetFileNameWithoutExtension(f).ToLower()))
                where (options.SFX ? true : !SFX.Contains(Path.GetFileNameWithoutExtension(f).ToLower()))
                select f;

            if (!musicOGGs.Any())
            {
                return new RandoResult("No valid music OGG files found .", false);
            }
            else
            {
                try //attempt to make a backup folder
                {

                        temp = "Randomization complete!";
                        temp += " Really sorry, this randomizer doesn't have a backup feature.";
                        return new RandoResult(temp, true);
                    }
                }
            }
        }   
    }
