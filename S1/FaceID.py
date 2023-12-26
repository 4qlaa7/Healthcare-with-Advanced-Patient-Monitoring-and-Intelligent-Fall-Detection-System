import cv2
import numpy as np
import face_recognition
import os


path = 'Persons'  # put the path
images = []
classNames = []
personsList = os.listdir(path)
reconame = ""

for cl in personsList:
    curPersonn = cv2.imread(f'{path}/{cl}')
    images.append(curPersonn)
    classNames.append(os.path.splitext(cl)[0])

print(images)
print(classNames)
print("ClassNames Done!")


def findEncodeings(images):
    encodeList = []
    for img in images:
        img = cv2.cvtColor(img,cv2.COLOR_BGR2RGB)
        encode = face_recognition.face_encodings(img)[0]
        encodeList.append(encode)
    return encodeList

encodeListKnown = findEncodeings(images)
print('Encoding Complete.')


def takeframe():
    framect = 0
    cap = cv2.VideoCapture(0)
    while cap.isOpened:
        ret, frame = cap.read()
        framect +=1
        if ret and framect == 3:
            return frame



# This Function Takes ndarray OK !!!! 

def who(face):
    #face = "D:/EDU/MSA/CS 484 HCI/Project\Healthcare-with-Advanced-Patient-Monitoring-and-Intelligent-Fall-Detection-System\Senario one with Tuio/Persons/sherif.jpg"
    cap = cv2.imwrite('face.jpg', face)
    cap = cv2.imread('face.jpg')
    
    imgS = cv2.resize(cap, (0,0), None, 0.25, 0.25)
    imgS = cv2.cvtColor(imgS, cv2.COLOR_BGR2RGB)

    faceCurentFrame = face_recognition.face_locations(imgS)
    encodeCurentFrame = face_recognition.face_encodings(imgS, faceCurentFrame)

    for encodeface, faceLoc in zip(encodeCurentFrame, faceCurentFrame):
        matches = face_recognition.compare_faces(encodeListKnown, encodeface)
        faceDis = face_recognition.face_distance(encodeListKnown, encodeface)
        matchIndex = np.argmin(faceDis)

        if matches[matchIndex]:
            reconame = classNames[matchIndex].upper()
            print(reconame)
            break
    return "True",reconame


