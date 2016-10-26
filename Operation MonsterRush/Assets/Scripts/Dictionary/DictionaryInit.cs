using System.Linq;
using System.Collections.Generic;

/*public class SerializableDictionary <TKey, TValue> : ISerializationCallbackReceiver
{
	private Dictionary <TKey, TValue> _dictionary;
	[SerializeField] List <TKey> _keys;
	[SerializeField] List <TValue> _values;

	public void OnBeforeSerialize()
	{
		_keys.Clear();
		_values.Clear();

		foreach (KeyValuePair <TKey, TValue> pair in this)
		{
			this.Add (pair.Key, pair.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		this.Clear();

		if (_keys.Count != _values.Count)
		{
			throw new System.Exception (string.Format ("There are {0} keys and {1} values after deserialization.", _keys.Count, _values.Count));
		}

		for (int i = 0; i < _keys.Count; i++)
		{
			this.Add (_keys[i], _values[i]);
		}
	}
}*/
