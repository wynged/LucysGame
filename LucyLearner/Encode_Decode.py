def encode_one_hot(value, highestNumber):
    vec = [0]*(highestNumber+1)
    if value is not None:
        vec[value] = 1
    return vec

def get_choice_val_diff(vec):
    val = vec.min()
    choice = vec.argmin()
    return choice, val, val-vec.max()


def scale_value(val, minVal, maxMal):
    return  

def encode_one_hot_multiple(values, highestNumber):
    vecs = []
    for v in values:
        vecs.append(encode_one_hot(v, highestNumber))

    return vecs