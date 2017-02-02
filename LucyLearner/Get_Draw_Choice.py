from PredictWithNN import DrawCardChoice

import sys
cardVal = int(sys.argv[1])
print "val is: ", cardVal

chosenAction, value, diff = DrawCardChoice(cardVal)

#print chosenAction, ":", value, ":", diff
print chosenAction