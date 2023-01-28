using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public struct SettingsConfig
    {
        [SerializeField] private SettingsSection _video;
        [SerializeField] private SettingsSection _sound;
        [SerializeField] private SettingsSection _game;
        
        public SettingsSection Video => _video;
        public SettingsSection Sound => _sound;
        public SettingsSection Game => _game;

        public SettingsSection GetSection(SettingsSectionLabel label)
        {
            return label switch
            {
                SettingsSectionLabel.Video => Video,
                SettingsSectionLabel.Sound => Sound,
                SettingsSectionLabel.Game => Game,
                _ => throw new ArgumentOutOfRangeException(nameof(label), label, null)
            };
        }
    }

    public enum SettingsSectionLabel
    {
        Video,
        Sound,
        Game
    }
}