import { use, useState,useEffect } from "react";


function App() {
  const [applications, setApplication] = useState([]);
  
  useEffect(() => {
    fetch("http://localhost:5000/applications")
      .then((response) => response.json())
      .then((data) => setApplication(data));
  }, []);
  return (
    <div className="App">
      <h1>Applications</h1>
      <ul>
        {applications.map((app) => (
          <li key={app.id}>{app.company}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
