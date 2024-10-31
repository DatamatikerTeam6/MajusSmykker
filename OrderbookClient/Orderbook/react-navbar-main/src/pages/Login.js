import React, { useState } from "react";
import { Helmet } from "react-helmet";
import { Button } from "../components/button/Button";
import { Heading }  from "../components/heading/Heading";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleLogin = async event => {
    event.preventDefault();
    try {
      const response = await fetch("https://localhost:7187/api/Accounts/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      if (response.ok) {
        const data = await response.json();
        const token = data.token;
        localStorage.setItem("token", token);
        alert("Login successful!");
      } else {
        setErrorMessage("Invalid email or password.");
      }
    } catch (error) {
      console.error("Login error:", error);
      setErrorMessage("An error occurred. Please try again.");
    }
  };

  return (
    <>
      <Helmet>
        <title>User Login - Access Your Account</title>
        <meta
          name="description"
          content="Log in to your account using your email and password. Secure and easy access to manage your account and services."
        />
      </Helmet>
      <div className="flex w-full flex-col items-start justify-center gap-16 bg-white-a700 py-[146px] pl-[690px] pr-14 lg:px-0">
        <Heading
          size="texts"
          as="h1"
          className="ml-[90px] text-[64px] font-medium tracking-[-1.41px] text-gray-900 lg:text-[48px] md:ml-0 md:text-[48px]"
        >
          Log ind
        </Heading>
        <form onSubmit={handleLogin} className="w-[46%] lg:w-full md:w-full">
          <div className="mb-[244px] mt-16 flex flex-col items-start gap-[76px] lg:gap-[57px] md:gap-[57px] sm:gap-[38px]">
            <div className="flex flex-col items-start justify-center self-stretch">
              <Heading as="h2" className="text-[20px] font-medium tracking-[-0.22px] text-black-900 lg:text-[17px]">
                Email
              </Heading>
              <input
                type="email"
                value={email}
                onChange={event => setEmail(event.target.value)}
                required
                className="h-[54px] w-[76%] rounded border-solid border-black-900"
              />
            </div>

            <div className="flex flex-col items-start justify-center self-stretch">
              <Heading as="h3" className="text-[20px] font-medium tracking-[-0.22px] text-black-900 lg:text-[17px]">
                Kodeord
              </Heading>
              <input
                type="password"
                value={password}
                onChange={event => setPassword(event.target.value)}
                required
                className="h-[54px] w-[76%] rounded border-solid border-black-900"
              />
            </div>
            
            {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
            <div>
            <Button
              shape="round"
              className="ml-[134px] min-w-[126px] rounded px-[30px] tracking-[-0.44px] sm:ml-0 sm:px-4"
              type="submit"
            >
              Log ind
            </Button>
            </div>
          </div>
        </form>
      </div>
    </>
  );
}
