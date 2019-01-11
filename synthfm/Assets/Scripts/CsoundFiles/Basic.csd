<Cabbage>
form caption("Test"), size(300, 200)
</Cabbage>
<CsoundUnity>
form caption("SimpleFreq"),
hslider channel("freq1"), range(0,1000,0), text("Frequency Slider")
button channel("trigger"), text("Push me")
checkbox channel("Mute")
</CsoundUnity>
<CsoundSynthesizer>
<CsOptions>
-n -d -m0d
</CsOptions>
<CsInstruments>
sr 	= 	44100
ksmps 	= 	32
nchnls 	= 	2
0dbfs	=	1

instr PLAY_AUDIO_CLIP
a1 inch 1
a2 inch 2
outs a1, a2

;uncomment for fx..
;outs comb:a(a1, 2, .5), comb:a(a2, 2, .4)

endin

</CsInstruments>
<CsScore>
i"PLAY_AUDIO_CLIP" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>
