
# create ramp
ramp = list(range(255, 0, -1)) + list(range(255))
STEP = 5
for i in range(0, len(ramp), STEP):
    
    # HSV with 300|160|x (pink hue), since green is dominating
    setLED(1, HSV=[300, 160, ramp[0]])
    setLED(2, HSV=[300, 160, ramp[100]])
    setLED(3, HSV=[300, 160, ramp[200]])
    setLED(4, HSV=[300, 160, ramp[300]])
    
    # rotate ramp
    ramp = ramp[STEP:] + ramp[:STEP]
    sleep(0.02) 

