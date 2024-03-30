using System;

[Serializable]
public class Settings
{
    public bool one_button;
    public float offset;
    public int master_vol;
    public int music_vol;
    public int effect_vol;
    public bool visuals;
    public bool no_fail;

    public Settings(bool one_button, float offset, int master_vol, int music_vol, int effect_vol, bool visuals, bool no_fail)
    {
        this.one_button = one_button;
        this.offset = offset;
        this.master_vol = master_vol;
        this.music_vol = music_vol;
        this.effect_vol = effect_vol;
        this.visuals = visuals;
        this.no_fail = no_fail;
    }
}