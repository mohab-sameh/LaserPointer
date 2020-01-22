import cv2
import numpy as np
from dollarpy import Recognizer, Template, Point
cap = cv2.VideoCapture(0)

found = False
pts = []
xpts =[]
ypts =[]
while (1):

    # Take each frame
    ret, frame = cap.read()
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

    lower_red = np.array([0, 255, 0])
    upper_red = np.array([255, 255, 255])
    mask = cv2.inRange(hsv, lower_red, upper_red)
    (minVal, maxVal, minLoc, maxLoc) = cv2.minMaxLoc(mask)

    cv2.circle(frame, maxLoc, 20, (0, 0, 255), 2, cv2.LINE_AA)
    
    cv2.imshow('Track Laser', frame)
    res = cv2.bitwise_and(frame, frame, mask-mask)
    #cv2.imshow('ret', res)

    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

    if maxLoc[0] == 0 and maxLoc[1] == 0:
        found = False
        del xpts[:]
        del ypts[:]
        xpts.clear
        ypts.clear
    else:
        found = True
        print("pointer found at X: " , maxLoc[0], "pointer found at Y: ", maxLoc[1])
        xpts.append(maxLoc[0])
        ypts.append(maxLoc[1])
    
    #print(len(xpts))
    #print(len(ypts))

cap.release()
cv2.destroyAllWindows()