import socket
import threading
import cv2
from FaceID import who,takeframe
from Eye_Gaze_Tracking import eyegaze
from expressions import expression
from HandGestures import reco
import struct
# from s1.dollarpy import test,traindata



mySocket = socket.socket()
mySocket.bind(('localhost', 4344))
mySocket.listen(5)

def connector():
    print("wait")
    while True:
        conn, addr = mySocket.accept()
        print("Device connected:", addr)
        # Temps = traindata()
        threading.Thread(target=listen, args=(conn,)).start()
        threading.Thread(target=cameraread)
        #threading.Thread(target=send, args=(conn,)).start()

def send(conn):
    print("Sending..")
    file_path = "D:/EDU/MSA/CS 484 HCI/Project/Healthcare-with-Advanced-Patient-Monitoring-and-Intelligent-Fall-Detection-System/Senario one with Tuio/Persons/ayman.jpg"
    #face = who(file_path)
    #print("Face:", face)
    msg = bytes(file_path, 'utf-8')
    conn.send(msg)

def Send_farmes(img_str):
        global isok
        if isok:
            global conn
            conn.sendall(struct.pack("Q", len(img_str)))    
            conn.sendall(img_str)


cap = cv2.VideoCapture(0)
def cameraread():
    while True:
        # Read a frame from the camera
        ret, frame = cap.read()    
        # Encode the frame as JPEG
        encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]
        _, img_encoded = cv2.imencode('.jpg', frame, encode_param)
        img_str = img_encoded.tobytes()
        Send_farmes(img_str)

def listen(conn):
    print("Receiving..")
    while True:
        data = conn.recv(1024).decode('utf-8')
        message = ""
        if not data:
            break
        if ',' in data:
            _, message = data.split(",", 1)
        else:
            # Handle the case where there is no comma in the data
            print("Invalid data format:", data)
        #print(message)
        if message == "FACEID":
            face = takeframe()
            #face = cv2.imread("face.jpg")
            boool,recognize = who(face)
            #print("Face:", face)
            d = f'FACEID,{boool},{recognize}'
            print(d)
            d = bytes(d, 'utf-8')
            conn.send(d)
        
        if message == "GAZE":
            webcam = cv2.VideoCapture(0)
            while webcam.isOpened:
                pos = eyegaze(webcam)
                print(pos)
                d = f'GAZE,{pos}'
                #print(d)
                d = bytes(d, 'utf-8')
                conn.send(d)

        if message == "EXPR":
            exp = expression()
            d = f'EXPR,{exp}'
            d = bytes(d, 'utf-8')
            conn.send(d)
        
        if message == "GESTURE":
            reco(conn)


        if message =="FALL":
            print("mewo")
            # fall = test(Temps)
            # d = f'FALL,{fall}'
            # d = bytes(d, 'utf-8')
            # conn.send(d)
    
# Start the connector thread
            

thread1 = threading.Thread(target=connector)
thread1.start()

# Wait for the connector thread to finish
thread1.join()
