import socket
import threading
import cv2
from FaceID import who,takeframe
from Eye_Gaze_Tracking import eyegaze

mySocket = socket.socket()
mySocket.bind(('localhost', 4344))
mySocket.listen(5)

def connector():
    while True:
        conn, addr = mySocket.accept()
        print("Device connected:", addr)
        threading.Thread(target=listen, args=(conn,)).start()
        #threading.Thread(target=send, args=(conn,)).start()

def send(conn):
    print("Sending..")
    file_path = "D:/EDU/MSA/CS 484 HCI/Project/Healthcare-with-Advanced-Patient-Monitoring-and-Intelligent-Fall-Detection-System/Senario one with Tuio/Persons/ayman.jpg"
    #face = who(file_path)
    #print("Face:", face)
    msg = bytes(file_path, 'utf-8')
    conn.send(msg)


def listen(conn):
    print("Receiving..")
    while True:
        data = conn.recv(1024).decode('utf-8')
        if not data:
            break
        _, message = data.split(",",1)
        print(message)
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
            print("sss")

# Start the connector thread
thread1 = threading.Thread(target=connector)
thread1.start()

# Wait for the connector thread to finish
thread1.join()
