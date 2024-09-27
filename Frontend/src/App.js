import './styles/App.css';
import SubjectCrud from './SubjectCrud.js';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Correct import from react-router-dom
import Home from './Home';
import PageHeader from './PageHeader.js';

function App() {
  return (
    <>
      <div className="app-container">
        <Router>
          <PageHeader/>
          <div className="main-content">
            <Routes>
              <Route path="/" element={<Home/>}/>
              <Route path="/subjectcrud" element={<SubjectCrud/>}/>
            </Routes>
          </div>
        </Router>
      </div>
    </>
  );
}

export default App;