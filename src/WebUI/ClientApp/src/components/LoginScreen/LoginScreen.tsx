import "./LoginScreen.css"

interface LoginScreenProps {
    handleLogin: () => void;
}

export const LoginScreen: React.FC<LoginScreenProps> = (props: LoginScreenProps) => {
    return (
        <div className="wrapper">
            <h1>Welcome to MiniRent!</h1>
            <div className="sign-in-box">
                <div className="sign-in-text">Sign in using Microsoft account</div>
                <button type="button" className="btn btn-primary sign-in-button"
                    onClick={props.handleLogin}>
                    Sign in
                </button>
            </div>
        </div>
    )
}