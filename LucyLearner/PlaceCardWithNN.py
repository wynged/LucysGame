from keras.models import Sequential, model_from_json
import numpy as np
import Encode_Decode

import sys
import ast

HIGHEST_NUM_IN_DECK = 12
LEARNER_FILE_PATH = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\LucyLearner\\"

def LoadPlaceCardChoiceModel():
    # load json and create model
    json_file = open(LEARNER_FILE_PATH+"model/Place_Card_Model.json", 'r')
    loaded_model_json = json_file.read()
    json_file.close()
    loaded_model = model_from_json(loaded_model_json)
    # load weights into new model
    loaded_model.load_weights(LEARNER_FILE_PATH+"model/Place_Card_Model.h5")
    print("Loaded model from disk")
    return loaded_model

def PlaceCardChoice(state):
    situationArray = Encode_Decode.encode_one_hot_multiple(state, HIGHEST_NUM_IN_DECK)
    model = LoadPlaceCardChoiceModel()
    model.compile(optimizer='adam',
              loss='mse')
    print situationArray
    predictionVec = model.predict(np.array([situationArray]))
    return Encode_Decode.get_choice_val_diff(predictionVec)



chosenAction, value, diff = PlaceCardChoice([1,3,None,4,1]) # ast.literal_eval(sys.argv[1]))

print "DO: {0}  GuessValue: {1}, DiffMaxToMin: {2}".format(chosenAction, value, diff)