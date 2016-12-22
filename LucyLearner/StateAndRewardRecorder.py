import json
from keras.models import Sequential


filepath = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\recordedGameplay\\tests - Copy.txt"

opened = open(filepath)

all = opened.readlines();

print "Num Lines:", len(all) 

for a in all:
    jsoned = json.loads(a)
    print jsoned
    print "---NEXT---"
    #print jsoned["PlayerCards"]
    