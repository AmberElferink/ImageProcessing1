1. Grayscale: neem 1 RGB-value en maak alle values dat (of neem gemiddelde van RGB-values en maak alle values dat (vergeet niet af te ronden))
2. Contrast adjustment: a-low = a-min, a-high = a-max
3. Gaussian filter: Toepassen 3x3 matrix met Gaussian verdeling (sigma afhankelijk)
4. Linear filtering: Gewoon boxfilter
5. Nonlinear filtering: min-max filter of median filter
6. Edge detection: filter dat laat zien waar en hoe sterk een edge zit
7. Thresholding: Neem een threshold, a < threshold -> a=0, a >= threshold -> a=255

Images:
1. -> 2. : Image A
A -> 3. -> 6. -> 7. : Image B
A -> 5. -> 6. -> 7. : Image C
A -> 3. (increasing kernel size) -> 6. -> 7. : Image D1-D5

Bonus:
1. Histogram equalization functie -> behandeld in lecture 2
2. Edge sharpening functie -> max filter? Anders wordt nog behandeld

Report:
1. Wat zijn de verschillen tussen A t/m D5?
2. Waarom hebben we die specifieke waardes gekozen om B en C te krijgen?
3. Wat is de trend van D1-D5 en waarom?

Deadline: zondag 23 september 23:00

UU-Git Personal Access Token code: iXvWPsdrXuAxuGtSu5fa