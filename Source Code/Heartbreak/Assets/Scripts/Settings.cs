using System;

[Serializable]
public class Settings
{
    public bool one_button;
    public float offset;
    public int scroll_mult;
    public int master_vol;
    public int music_vol;
    public int effect_vol;
    public bool no_fail;
    public bool visuals;
    public bool metronome;
    public bool extra_timing;

    public Settings(bool one_button, float offset, int scroll_mult, int master_vol, int music_vol, int effect_vol, bool no_fail, bool visuals, bool metronome, bool extra_timing)
    {
        this.one_button = one_button;
        this.offset = offset;
        this.scroll_mult = scroll_mult;
        this.master_vol = master_vol;
        this.music_vol = music_vol;
        this.effect_vol = effect_vol;
        this.no_fail = no_fail;
        this.visuals = visuals;
        this.metronome = metronome;
        this.extra_timing = extra_timing;
    }
}