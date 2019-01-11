<CsoundUnity>
form caption("SimpleFreq"),
hslider channel("freq1"), range(0,1000,0), text("Frequency Slider")
button channel("trigger"), text("Push me")
checkbox channel("Mute")
</CsoundUnity>
<CsoundSynthesizer>\
<CsOptions>\
-odac -n -d -m0d\
</CsOptions>\
<CsInstruments>\
;Example by Alex Hofmann\
instr 1\
aSin\'a0\'a0\'a0   oscils    0dbfs/4, 440, 0\
          out       aSin\
endin\
</CsInstruments>\
<CsScore>\
i 1 0 1\
</CsScore>\
</CsoundSynthesizer>}
