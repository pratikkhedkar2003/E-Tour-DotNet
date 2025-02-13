import { Link } from "react-router-dom";


const NotFound = () => {
  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100 text-center">
      <h1 className="text-6xl font-bold text-gray-800">404</h1>
      <p className="text-xl text-gray-600 mt-4">
        Oops! The page you&apos;re looking for doesn&apos;t exist.
      </p>
      <Link to="/home">
        <button className="mt-6 px-6 py-3 text-lg">Go Home</button>
      </Link>
    </div>
  );
};

export default NotFound;
