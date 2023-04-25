package com.example.mouseapplication;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;

import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;

public class TouchpadView extends View {
    private float xCord, yCord;
    final String ipAddress = "10.100.102.74";
    final int port = 12345;


    public TouchpadView(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
            case MotionEvent.ACTION_MOVE:
            case MotionEvent.ACTION_UP:
                float x = event.getX();
                float y = event.getY();
                if (x != xCord || y != yCord)
                    sendCoordinatesToWindowsApp(x, y);
                xCord = x;
                yCord = y;
                break;
        }
        return true;
    }

    private void sendCoordinatesToWindowsApp(float x, float y) {
        Log.d("TAG", "sendCoordinatesToWindowsApp: TOUCH (" + x + "," + y + ")");
        // Implement your code to send the coordinates to your Windows application via USB.
        sendTouchpadData(ipAddress,port,"("+x+","+y+")");
    }

    public void sendTouchpadData(String ipAddress, int port, String touchpadData) {
        new Thread(() -> {
            try {
                Socket socket = new Socket(ipAddress, port);
                OutputStream outputStream = socket.getOutputStream();
                outputStream.write(touchpadData.getBytes());
                outputStream.close();
                socket.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }).start();
    }
}