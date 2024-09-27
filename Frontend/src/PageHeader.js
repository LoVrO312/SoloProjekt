import { Link } from 'react-router-dom';

function PageHeader() {
    return (
        <nav className="page-header">
            <Link to="/">
                <button>Home</button>
            </Link>
            <Link to="/subjectcrud">
                <button>Subject CRUD</button>
            </Link>
        </nav>
    );
}

export default PageHeader;
