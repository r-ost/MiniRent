import "./LoginScreen.css"
import { Button, DatePicker } from 'antd';
import 'antd/dist/antd.css';

interface LoginScreenProps {
    handleLogin: () => void;
}

export const LoginScreen: React.FC<LoginScreenProps> = (props: LoginScreenProps) => {
    return (
        <div className="wrapper">
            <h1>Welcome to MiniRent!</h1>
            <div className="sign-in-box">
                <div className="sign-in-text">Sign in using Microsoft account</div>
                <Button type="primary" className="w-64"
                    onClick={props.handleLogin}>
                    Sign in
                </Button>
            </div>
        </div>
    )
}