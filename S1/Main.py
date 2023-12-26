import socket
import threading
from FaceID import who 

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

        _, message = data.split(",", 1)
        print(message)
        if message == "FACEID":
            file_path = "D:/EDU/MSA/CS 484 HCI/Project/Healthcare-with-Advanced-Patient-Monitoring-and-Intelligent-Fall-Detection-System/Senario one with Tuio/Persons/ayman.jpg"
            #face = who(file_path)
            #print("Face:", face)
            d = "FACEID,True"
            print(d)
            d = bytes(d, 'utf-8')
            conn.send(d)

# Start the connector thread
thread1 = threading.Thread(target=connector)
thread1.start()

# Wait for the connector thread to finish
thread1.join()
