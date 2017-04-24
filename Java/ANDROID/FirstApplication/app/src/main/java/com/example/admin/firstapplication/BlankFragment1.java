package com.example.admin.firstapplication;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
//import android.support.v4.app.Fragment;
import android.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;


public class BlankFragment1 extends Fragment implements View.OnClickListener {

    OnButtonClickEventListener mCallback;

    @Override
    public void onClick(View v) {
        mCallback.OnFragmentButtonClick("!!!!!!!!");
    }

    // Container Activity must implement this interface
    public interface OnButtonClickEventListener {
        public void OnFragmentButtonClick(String newText);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        mCallback = (OnButtonClickEventListener) activity;
        Button button = (Button) activity.findViewById(R.id.sendMessageToAnotherFragmentButton);
        button.setOnClickListener(this);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_blank_fragment1, container, false);
    }

    public void SetText(String newText){
        TextView TextViewInThisActivity = (TextView)getView().findViewById(R.id.TextViewB);
        TextViewInThisActivity.setText(newText);
    }
}
