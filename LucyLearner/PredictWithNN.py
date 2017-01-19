from keras.models import Sequential, model_from_json
import numpy as np

def LoadSavedModel():
    # load json and create model
    json_file = open('model/model.json', 'r')
    loaded_model_json = json_file.read()
    json_file.close()
    loaded_model = model_from_json(loaded_model_json)
    # load weights into new model
    loaded_model.load_weights("model/model.h5")
    print("Loaded model from disk")
    return loaded_model

def DrawCardChoice():
    model = LoadSavedModel()
    model.compile(optimizer='rmsprop',
              loss='categorical_crossentropy',
              metrics=['accuracy'])
    situation = np.array([[1,0,0,0,0,0,0,0,0,0,0,0,0]])
    print model.predict(situation)

DrawCardChoice()

