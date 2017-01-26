from keras.models import Sequential, model_from_json
import numpy as np
import Encode_Decode

HIGHEST_NUM_IN_DECK = 12
LEARNER_FILE_PATH = "C:\\Users\\erudisaile\\Documents\\_code\\_gitHub\\LucysGame\\LucyLearner\\"

def LoadDrawChoiceModel():
    # load json and create model
    json_file = open(LEARNER_FILE_PATH+"model/model.json", 'r')
    loaded_model_json = json_file.read()
    json_file.close()
    loaded_model = model_from_json(loaded_model_json)
    # load weights into new model
    loaded_model.load_weights(LEARNER_FILE_PATH+"model/model.h5")
    print("Loaded model from disk")
    return loaded_model

def DrawCardChoice(drawNumber):
    situationArray = Encode_Decode.encode_one_hot(drawNumber, HIGHEST_NUM_IN_DECK)
    model = LoadDrawChoiceModel()
    model.compile(optimizer='rmsprop',
              loss='mse')
    print situationArray
    predictionVec = model.predict(np.array([situationArray]))
    return Encode_Decode.get_choice_val_diff(predictionVec)



chosenAction, value, diff = DrawCardChoice(12)

print "DO: {0}  GuessValue: {1}, DiffMaxToMin: {2}".format(chosenAction, value, diff)