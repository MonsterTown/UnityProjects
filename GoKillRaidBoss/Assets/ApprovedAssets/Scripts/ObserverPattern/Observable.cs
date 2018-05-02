public interface Observable  {

    void AddObserver(Observer o);
    void RemoveObserver(Observer o);
    void NotifyObservers();
}
