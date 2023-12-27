import pandas as pd
import numpy as np
import seaborn as sns
import cv2
import matplotlib.pyplot as plt


filepath="gaze_tracking_coordinates.csv"
def heat_map(img_path):
    image_path=img_path
    eye_data = pd.read_csv(filepath, header=None, names=['X', 'Y'])
    img = cv2.imread(image_path)
    copy=img.copy()

    img = cv2.cvtColor(copy, cv2.COLOR_BGR2RGB)

    fig, ax = plt.subplots(figsize=(10, 8))
    ax.imshow(img, extent=[0, img.shape[1], 0, img.shape[0]],
            origin='upper')
    sns.kdeplot(x=eye_data['X'], y=eye_data['Y'], fill=True,
                cmap='rainbow', cbar=False, ax=ax, alpha=0.3)
    ax.set_xticks([])
    ax.set_yticks([])
    ax.set_frame_on(False)
    cv2.imwrite("Healthcare-with-Advanced-Patient-Monitoring-and-Intelligent-Fall-Detection-System\S1\Eye_Gaze_Tracking/heatmap_plot_with_image.png", img)

    img=cv2.imread('heatmap_plot_with_image.png')
    cv2.imshow('heatmap_plot_with_image.png', img)

   
